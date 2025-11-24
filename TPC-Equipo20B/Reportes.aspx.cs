using Dominio;
using Negocio;
using System;
using System.Linq;

namespace TPC_Equipo20B
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Permisos.RequiereAdmin(this);

            if (IsPostBack) return;

            CargarReportes();
        }

        private void CargarReportes()
        {
            var ventaNegocio = new VentaNegocio();

            decimal totalMes = ventaNegocio.ObtenerTotalVentasMes(null);
            int pedidosMes = ventaNegocio.ObtenerPedidosCompletadosMes(null);
            int clientesMes = ventaNegocio.ObtenerClientesNuevosMes(null);
            decimal ticketPromedio = ventaNegocio.ObtenerTicketPromedioMes(null);

            lblRptVentasMes.Text = totalMes.ToString("C");
            lblRptPedidos.Text = pedidosMes.ToString();
            lblRptClientes.Text = clientesMes.ToString();
            lblRptTicket.Text = ticketPromedio.ToString("C");

            var top = ventaNegocio.TopProductosVendidosMes(null)
                .Select(p => new
                {
                    p.Producto,
                    p.Categoria,
                    p.Unidades,
                    p.Ingresos,
                    p.Vendedor
                })
                .ToList();

            gvTopProductos.DataSource = top;
            gvTopProductos.DataBind();
        }
    }
}
