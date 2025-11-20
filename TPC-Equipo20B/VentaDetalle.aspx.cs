using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class VentaDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idVenta = int.Parse(Request.QueryString["id"]);
                    CargarVenta(idVenta);
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
            }
            else
                panelCancelada.Visible = false;


            lblCliente.Text = venta.Cliente?.Nombre ?? "-";
            lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");
            lblMetodoPago.Text = venta.MetodoPago ?? "-";
            lblFactura.Text = venta.NumeroFactura ?? "-";
            lblTotal.Text = venta.Total.ToString("C");

            gvLineas.DataSource = venta.Lineas;
            gvLineas.DataBind();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }
    }
}
