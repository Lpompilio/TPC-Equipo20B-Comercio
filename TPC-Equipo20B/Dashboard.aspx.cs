using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void CargarResumenGeneral()
        {
            // Pendiente: Mostrar totales de ventas, pedidos y clientes nuevos
        }

        private void CargarStockBajo()
        {
            // Pendiente: Cargar gvStockBajo con los productos de bajo stock
        }

        private void CargarUltimasVentas()
        {
            // Pendiente: Cargar gvUltimasVentas con las ventas más recientes
        }
    }
}