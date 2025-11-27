using System;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class VentaDetalle : System.Web.UI.Page
    {
        public string NombreComprobante { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idVenta = int.Parse(Request.QueryString["id"]);
                    CargarVenta(idVenta);
                }
                else
                {
                    Response.Redirect("Ventas.aspx");
                }
            }
        }

        private void CargarVenta(int id)
        {
            VentaNegocio negocio = new VentaNegocio();
            Venta venta = negocio.ObtenerPorId(id);

            if (venta == null)
            {
                lblCliente.Text = "No encontrada";
                return;
            }

            if (venta.Cancelada)
            {
                panelCancelada.Visible = true;
                lblMotivo.Text = venta.MotivoCancelacion;
                lblFechaCanc.Text = venta.FechaCancelacion?.ToString("dd/MM/yyyy HH:mm");
                lblUsuarioCanc.Text = venta.UsuarioCancelacion?.Nombre ?? "(no encontrado)";
                lblNC.Text = string.IsNullOrEmpty(venta.NumeroNC) ? "-" : venta.NumeroNC;
                btnImprimir.Text = "Imprimir NC / PDF";
            }
            else
            {
                panelCancelada.Visible = false;
                btnImprimir.Text = "Imprimir Remito / PDF";
            }

            lblCliente.Text = venta.Cliente?.Nombre ?? "-";
            lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");
            lblMetodoPago.Text = venta.MetodoPago ?? "-";
            lblFactura.Text = venta.NumeroFactura ?? "-";
            lblTotal.Text = venta.TotalBD.ToString("C");
            lblVendedor.Text = venta.Usuario?.Nombre ?? "-";

            gvLineas.DataSource = venta.Lineas;
            gvLineas.DataBind();

            string nombreArchivo;

            if (venta.Cancelada)
            {
                nombreArchivo = !string.IsNullOrEmpty(venta.NumeroNC)
                    ? venta.NumeroNC
                    : "NotaCredito";
            }
            else
            {
                nombreArchivo = !string.IsNullOrEmpty(venta.NumeroFactura)
                    ? venta.NumeroFactura
                    : "Remito";
            }

            if (nombreArchivo == null)
                nombreArchivo = string.Empty;

            NombreComprobante = nombreArchivo.Replace("'", "");
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }

        // --------------------------------------------------------------------
        // 📧 BOTÓN REENVIAR MAIL (VERSIÓN PERSONALIZADA)
        // --------------------------------------------------------------------
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta;
                if (!int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "MailError",
                        "alert('No se pudo determinar la venta para reenviar el mail.');",
                        true
                    );
                    return;
                }

                VentaNegocio negocio = new VentaNegocio();
                Venta venta = negocio.ObtenerPorId(idVenta);

                if (venta == null || venta.Cliente == null || string.IsNullOrEmpty(venta.Cliente.Email))
                {
                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "MailError",
                        "alert('El cliente no tiene un correo electrónico cargado.');",
                        true
                    );
                    return;
                }

                // Enviamos el mail
                negocio.EnviarMailFactura(venta);

                // Armamos mensaje personalizado
                string tipoComprobante = venta.Cancelada ? "la nota de crédito" : "el remito";
                string numeroComprobante = venta.Cancelada
                    ? (string.IsNullOrEmpty(venta.NumeroNC) ? "" : " " + venta.NumeroNC)
                    : (string.IsNullOrEmpty(venta.NumeroFactura) ? "" : " " + venta.NumeroFactura);

                string mensaje = $"Se reenvi\u00f3 {tipoComprobante}{numeroComprobante} a {venta.Cliente.Nombre} ({venta.Cliente.Email}) correctamente.";

                // Escapamos comillas simples para que no rompa el JavaScript
                string mensajeJs = mensaje.Replace("'", "\\'");

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "MailOk",
                    $"alert('{mensajeJs}');",
                    true
                );
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "MailError",
                    "alert('Error al reenviar el correo: " + ex.Message.Replace("'", "\\'") + "');",
                    true
                );
            }
        }
    }
}
