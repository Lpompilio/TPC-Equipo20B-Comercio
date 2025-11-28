using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                var hoy = DateTime.Today;

              
                txtFecha.Text = hoy.ToString("yyyy-MM-dd");

                
                txtFecha.Attributes["max"] = hoy.ToString("yyyy-MM-dd");

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

          
            ddlProducto.Items.Clear();
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione proveedor --", "0"));
        }

        private void CargarProductosPorProveedor(int idProveedor)
        {
            ProductoNegocio prodNeg = new ProductoNegocio();
            ddlProducto.DataSource = prodNeg.listarPorProveedor(idProveedor);
            ddlProducto.DataTextField = "Descripcion";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione producto --", "0"));
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);

            if (idProveedor == 0)
            {
                ddlProducto.Items.Clear();
                ddlProducto.Items.Insert(0, new ListItem("-- Seleccione proveedor --", "0"));
                return;
            }

            CargarProductosPorProveedor(idProveedor);
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            
            if (ddlProducto.SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
                return;

            int idProveedorCompra = int.Parse(ddlProveedor.SelectedValue);
            int idProducto = int.Parse(ddlProducto.SelectedValue);

          
            ProductoNegocio prodNeg = new ProductoNegocio();
            bool pertenece = prodNeg.ProductoPerteneceAProveedor(idProducto, idProveedorCompra);

            if (!pertenece)
            {
                lblError.Text = "❌ El producto seleccionado no pertenece al proveedor elegido para esta compra.";
                lblError.Visible = true;
                return;
            }

         
            lblError.Visible = false;

       
            Producto prod = prodNeg.ObtenerPorId(idProducto);

         
            CompraLinea nueva = new CompraLinea
            {
                Producto = prod,
                Cantidad = int.Parse(txtCantidad.Text),
                PrecioUnitario = decimal.Parse(txtPrecio.Text)
            };

           
            Lineas.Add(nueva);

            
            ActualizarGrid();

            
            if (Lineas.Count == 1)
            {
                ddlProveedor.Enabled = false;
                txtFecha.Enabled = false;
            }

           
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecio.Text = "";
        }

        protected void gvLineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index;

                if (!int.TryParse(e.CommandArgument.ToString(), out index))
                    return;

                if (index < 0 || index >= Lineas.Count)
                    return;

                Lineas.RemoveAt(index);
                ActualizarGrid();

                
                if (Lineas.Count == 0)
                {
                    ddlProveedor.Enabled = true;
                    txtFecha.Enabled = true;
                }
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
            lblMensajeFooter.Visible = false;
            lblMensajeFooter.Text = "";

            bool proveedorVacio = ddlProveedor.SelectedValue == "0";
            bool sinProductos = (Lineas == null || Lineas.Count == 0);

            if (proveedorVacio || sinProductos)
            {
                lblMensajeFooter.Visible = true;
                lblMensajeFooter.Text = "Es necesario seleccionar un proveedor y agregar al menos un producto.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                lblMensajeFooter.Visible = true;
                lblMensajeFooter.Text = "Debe ingresar la fecha de la compra.";
                return;
            }

            DateTime fechaCompra;
            if (!DateTime.TryParse(txtFecha.Text, out fechaCompra))
            {
                lblMensajeFooter.Visible = true;
                lblMensajeFooter.Text = "La fecha de la compra no tiene un formato válido.";
                return;
            }

            if (fechaCompra.Date > DateTime.Today)
            {
                lblMensajeFooter.Visible = true;
                lblMensajeFooter.Text = "La fecha de la compra no puede ser futura.";
                return;
            }

            CompraNegocio negocio = new CompraNegocio();

            if (Session["Usuario"] == null)
            {
                Session["Usuario"] = new Usuario
                {
                    Id = 1,
                    Nombre = "Admin Temporal"
                };
            }

            Compra compra = new Compra
            {
                Proveedor = new Proveedor { Id = int.Parse(ddlProveedor.SelectedValue) },
                Fecha = fechaCompra,
                Usuario = (Usuario)Session["Usuario"],
                Observaciones = txtObservaciones.Text,
                Lineas = Lineas
            };

            negocio.Registrar(compra);

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
