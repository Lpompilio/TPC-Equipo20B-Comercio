using System;
using System.Linq;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDatos();
        }

        private void CargarDatos()
        {
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
            int? idUsuario = null;

            if (!esAdmin && Session["UsuarioId"] != null)
                idUsuario = (int)Session["UsuarioId"];

            var ventaNegocio = new VentaNegocio();
            var productoNegocio = new ProductoNegocio();

            decimal totalMes = ventaNegocio.ObtenerTotalVentasMes(idUsuario);
            int pedidosMes = ventaNegocio.ObtenerPedidosCompletadosMes(idUsuario);
            int clientesMes = ventaNegocio.ObtenerClientesNuevosMes(idUsuario);

            lblVentasMes.Text = totalMes.ToString("C");
            lblPedidosCompletados.Text = pedidosMes.ToString();
            lblClientesNuevos.Text = clientesMes.ToString();

            var stockBajo = productoNegocio.ListarStockBajo()
                .Select(p => new
                {
                    Codigo = p.CodigoSKU,
                    Producto = p.Descripcion,
                    StockActual = p.StockActual,
                    StockMinimo = p.StockMinimo
                })
                .ToList();

            gvStockBajo.DataSource = stockBajo;
            gvStockBajo.DataBind();

            // ==============================
            //  Últimas ventas (activas y canceladas)
            // ==============================
            System.Collections.Generic.IEnumerable<Dominio.Venta> listaVentas =
                ventaNegocio.Listar(null); // Traigo todas, activas y canceladas

            if (idUsuario.HasValue)
                listaVentas = listaVentas
                    .Where(v => v.Usuario != null && v.Usuario.Id == idUsuario.Value);

            var ultimas = listaVentas
                .OrderByDescending(v => v.Fecha)
                .Take(10)
                .Select(v => new
                {
                    IdVenta = v.Id,
                    Cliente = v.Cliente != null ? v.Cliente.Nombre : "",
                    Fecha = v.Fecha.ToString("dd/MM/yyyy"),
                    Total = v.TotalBD,
                    Vendedor = v.Usuario != null ? v.Usuario.Nombre : "",
                    Cancelada = v.Cancelada
                })
                .ToList();

            gvUltimasVentas.DataSource = ultimas;
            gvUltimasVentas.DataBind();
        }
    }
}
