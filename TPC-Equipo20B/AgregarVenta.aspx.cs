using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarVenta : System.Web.UI.Page
    {
        private List<VentaLinea> Lineas
        {
            get { return (List<VentaLinea>)(Session["VentaLineas"] ?? (Session["VentaLineas"] = new List<VentaLinea>())); }
            set { Session["VentaLineas"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CargarCombos();

                if (Request.QueryString["id"] != null)
                {
                    int idVenta = int.Parse(Request.QueryString["id"]);
                    CargarVenta(idVenta);
                }
            }
        }

        private void CargarCombos()
        {
            ClienteNegocio clienteNeg = new ClienteNegocio();
            ProductoNegocio productoNeg = new ProductoNegocio();

            ddlCliente.DataSource = clienteNeg.Listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            ddlProducto.DataSource = productoNeg.Listar();
            ddlProducto.DataTextField = "Descripcion";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProducto.SelectedValue == "0")
            {
                txtPrecio.Text = "";
                return;
            }

            int idProd = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio negocio = new ProductoNegocio();
            Producto prod = negocio.ObtenerPorId(idProd);

            txtPrecio.Text = prod.PrecioVenta.ToString("0.00");
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            lblErrorStock.Text = "";

            if (ddlProducto.SelectedValue == "0" || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text))
                return;

            ProductoNegocio productoNeg = new ProductoNegocio();
            Producto prod = productoNeg.Listar().FirstOrDefault(p => p.Id == int.Parse(ddlProducto.SelectedValue));

            if (prod == null)
            {
                lblErrorStock.Text = "❌ Error: producto no encontrado.";
                return;
            }

            int cantidad = int.Parse(txtCantidad.Text);

            if (cantidad > prod.StockActual)
            {
                lblErrorStock.Text = $"Stock insuficiente. Solo hay {prod.StockActual} unidades disponibles de \"{prod.Descripcion}\".";
                return;
            }

            VentaLinea nueva = new VentaLinea
            {
                Producto = prod,
                Cantidad = cantidad,
                PrecioUnitario = decimal.Parse(txtPrecio.Text)
            };

            Lineas.Add(nueva);
            gvLineas.DataSource = Lineas;
            gvLineas.DataBind();
            ActualizarTotal();

            txtCantidad.Text = "";
            ddlProducto.SelectedIndex = 0;
            txtPrecio.Text = "";
        }

        protected void gvLineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Lineas.RemoveAt(index);
                gvLineas.DataSource = Lineas;
                gvLineas.DataBind();
                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            decimal total = Lineas.Sum(l => l.PrecioUnitario * l.Cantidad);
            lblTotal.Text = total.ToString("C");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCliente.SelectedValue == "0" || Lineas.Count == 0)
                    return;

                int idUsuario = Session["UsuarioId"] != null ? (int)Session["UsuarioId"] : 0;

                Venta venta = new Venta
                {
                    Cliente = new Cliente { Id = int.Parse(ddlCliente.SelectedValue) },
                    Usuario = new Usuario { Id = idUsuario },
                    Fecha = DateTime.Now,
                    MetodoPago = ddlMetodoPago.SelectedValue,
                    Lineas = Lineas
                };

                VentaNegocio negocio = new VentaNegocio();
                // Registrar ya se encarga de guardar y enviar el mail
                negocio.Registrar(venta);

                Session["VentaLineas"] = null;
                Response.Redirect("Ventas.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al registrar la venta: " + ex.Message + "');</script>");
            }
        }

        private void CargarVenta(int idVenta)
        {
            VentaNegocio negocio = new VentaNegocio();
            Venta venta = negocio.ObtenerPorId(idVenta);

            ddlCliente.SelectedValue = venta.Cliente.Id.ToString();
            ddlMetodoPago.SelectedValue = venta.MetodoPago;
            txtFecha.Text = venta.Fecha.ToString("yyyy-MM-dd");

            Lineas = venta.Lineas;
            gvLineas.DataSource = Lineas;
            gvLineas.DataBind();
            ActualizarTotal();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }
    }
}
