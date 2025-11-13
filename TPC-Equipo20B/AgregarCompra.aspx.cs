using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarCompra : System.Web.UI.Page
    {
        private List<CompraLinea> Lineas
        {
            get
            {
                if (Session["LineasCompra"] == null)
                    Session["LineasCompra"] = new List<CompraLinea>();
                return (List<CompraLinea>)Session["LineasCompra"];
            }
            set { Session["LineasCompra"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Lineas = new List<CompraLinea>();
                ActualizarGrid();
            }
        }

        private void CargarCombos()
        {

            ProveedorNegocio provNeg = new ProveedorNegocio();
            ddlProveedor.DataSource = provNeg.Listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            ProductoNegocio prodNeg = new ProductoNegocio();
            ddlProducto.DataSource = prodNeg.Listar();
            ddlProducto.DataTextField = "Descripcion";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {

            if (ddlProducto.SelectedValue == "0" || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text))
                return;

            ProductoNegocio prodNeg = new ProductoNegocio();
            Producto prod = prodNeg.ObtenerPorId(int.Parse(ddlProducto.SelectedValue));

            CompraLinea nueva = new CompraLinea
            {
                Producto = prod,
                Cantidad = int.Parse(txtCantidad.Text),
                PrecioUnitario = decimal.Parse(txtPrecio.Text)
            };

            Lineas.Add(nueva);
            ActualizarGrid();

            // Limpiar campos
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecio.Text = "";
        }

        protected void gvLineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Lineas.RemoveAt(index);
                ActualizarGrid();
            }
        }

        private void ActualizarGrid()
        {
            gvLineas.DataSource = Lineas;
            gvLineas.DataBind();

            decimal total = 0;
            foreach (var l in Lineas)
                total += l.Subtotal;

            lblTotal.Text = total.ToString("C");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "0" || Lineas.Count == 0)
                return;

            CompraNegocio negocio = new CompraNegocio();

            Session["Usuario"] = new Usuario  // temporal, solo hasta que esté el manejo de usuarios
            {
                Id = 1,
                Nombre = "Admin Temporal"
            };


            Compra compra = new Compra
            {
                Proveedor = new Proveedor { Id = int.Parse(ddlProveedor.SelectedValue) },
                Fecha = DateTime.Now,
                Usuario = (Usuario)Session["Usuario"],
                Observaciones = txtObservaciones.Text,
                Lineas = Lineas
            };

            negocio.Registrar(compra);

            // Limpiar sesión y volver
            Session.Remove("LineasCompra");
            Response.Redirect("Compras.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("LineasCompra");
            Response.Redirect("Compras.aspx");
        }
    }
}
