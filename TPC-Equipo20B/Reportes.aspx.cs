using System;
using System.Data;

namespace TPC_Equipo20B
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            lblRptVentasMes.Text = "$ 25.480";
            lblRptPedidos.Text = "123";
            lblRptClientes.Text = "18";
            lblRptTicket.Text = "$ 2.070";

            var dt = new DataTable();
            dt.Columns.Add("Producto");
            dt.Columns.Add("Categoria");
            dt.Columns.Add("Unidades", typeof(int));
            dt.Columns.Add("Ingresos", typeof(decimal));

            dt.Rows.Add("Agua Purificada 20L", "Aguas", 2345, 58625m);
            dt.Rows.Add("Isotónica Naranja 600ml", "Bebidas", 1102, 19836m);
            dt.Rows.Add("Snack Mix 50g", "Snacks", 876, 10512m);

            gvTopProductos.DataSource = dt;
            gvTopProductos.DataBind();
        }
    }
}
