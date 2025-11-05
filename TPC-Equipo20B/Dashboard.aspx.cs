using System;
using System.Data;

namespace TPC_Equipo20B
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                lblVentasMes.Text = "$ 25.480";
                lblPedidosCompletados.Text = "42";
                lblClientesNuevos.Text = "7";

                gvStockBajo.DataSource = TablaStockBajo();
                gvStockBajo.DataBind();

                gvUltimasVentas.DataSource = TablaUltimasVentas();
                gvUltimasVentas.DataBind();
            }
        }

        private DataTable TablaStockBajo()
        {
            var t = new DataTable();
            t.Columns.Add("Codigo");
            t.Columns.Add("Producto");
            t.Columns.Add("StockActual");
            t.Columns.Add("StockMinimo");

            t.Rows.Add("AG-003", "Producto Ejemplo 3", "8", "15");
            t.Rows.Add("AG-004", "Producto Ejemplo 4", "0", "10");
            return t;
        }

        private DataTable TablaUltimasVentas()
        {
            var t = new DataTable();
            t.Columns.Add("IdVenta");
            t.Columns.Add("Cliente");
            t.Columns.Add("Fecha");
            t.Columns.Add("Total");
            t.Columns.Add("Estado");

            t.Rows.Add("78901", "Ana Torres", DateTime.Today.ToString("yyyy-MM-dd"), "$150.00", "Procesando");
            t.Rows.Add("78900", "Carlos Gomez", DateTime.Today.ToString("yyyy-MM-dd"), "$275.50", "Enviado");
            return t;
        }
    }
}
