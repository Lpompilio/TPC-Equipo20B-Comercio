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
            else
            {
                // Si ya hay líneas, bloquear cliente y método de pago
                if (Lineas.Count > 0)
                {
                    ddlCliente.Enabled = false;
                    ddlMetodoPago.Enabled = false;
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

            ddlProducto.DataSource = productoNeg.ListarHabilitados();
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
        protected void ValidarCantidad_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int cantidad;
            if (int.TryParse(args.Value, out cantidad) && cantidad > 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            lblErrorStock.Text = "";

            // Validar campos vacíos ANTES de ejecutar Page.Validate()
            if (ddlProducto.SelectedValue == "0" ||
                string.IsNullOrEmpty(txtCantidad.Text) ||
                string.IsNullOrEmpty(txtPrecio.Text))
                return;

            // Ahora sí validar cantidad
            Page.Validate("AgregarLinea");
            if (!Page.IsValid)
            {
                return; // Detener si hay errores de validación
            }

            ProductoNegocio productoNeg = new ProductoNegocio();
            int idProducto = int.Parse(ddlProducto.SelectedValue);

            // Traigo producto real de BD (stock real)
            Producto prod = productoNeg.ObtenerPorId(idProducto);
            if (prod == null)
            {
                lblErrorStock.Text = "❌ Error: producto no encontrado.";
                return;
            }

            int cantidadSolicitada = int.Parse(txtCantidad.Text);

            // Calcular cantidad ya agregada en esta venta para este producto
            decimal cantidadYaAgregada = Lineas
                .Where(l => l.Producto.Id == idProducto)
                .Sum(l => l.Cantidad);

            // Stock disponible real para esta venta
            int stockDisponible = (int)prod.StockActual - (int)cantidadYaAgregada;

            if (cantidadSolicitada > stockDisponible)
            {
                lblErrorStock.Text =
                    $"Stock insuficiente. Disponible: {stockDisponible} unidad(es) del producto \"{prod.Descripcion}\".";
                return;
            }

            // Si pasa la validación, agregamos la línea
            VentaLinea nueva = new VentaLinea
            {
                Producto = prod,
                Cantidad = cantidadSolicitada,
                PrecioUnitario = decimal.Parse(txtPrecio.Text)
            };

            Lineas.Add(nueva);
            //Bloquear cliente y método de pago una vez agregada la primera línea
            ddlCliente.Enabled = false;
            ddlMetodoPago.Enabled = false;

            gvLineas.DataSource = Lineas;
            gvLineas.DataBind();
            ActualizarTotal();

            // Limpiar campos
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

                if (Lineas.Count == 0)
                {
                    ddlCliente.Enabled = true;
                    ddlMetodoPago.Enabled = true;
                }
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

                bool clienteNoSeleccionado = ddlCliente.SelectedValue == "0";
                bool sinProductos = (Lineas == null || Lineas.Count == 0);

                if (clienteNoSeleccionado || sinProductos)
                {

                    lblMensajeFooter.Text = "Debe cargar los datos (Seleccione cliente y agregue productos)";
                    lblMensajeFooter.Visible = true;
                    return;
                }

                lblMensajeFooter.Visible = false;

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
                negocio.Registrar(venta);

                Session["VentaLineas"] = null;
                Response.Redirect("Ventas.aspx", false);
            }
            catch (Exception ex)
            {
  
                lblMensajeFooter.Text = "Error al registrar la venta: " + ex.Message;
                lblMensajeFooter.Visible = true;
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
