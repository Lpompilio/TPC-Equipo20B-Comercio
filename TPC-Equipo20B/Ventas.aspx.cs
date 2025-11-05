using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace TPC_Equipo20B
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            gvVentas.DataSource = VentasTabla();
            gvVentas.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            
            BindGrid();
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var dt = VentasTabla();
            var sb = new StringBuilder();

            // encabezados
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i > 0) sb.Append(";");
                sb.Append(dt.Columns[i].ColumnName);
            }
            sb.AppendLine();

            // filas
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(";");
                    sb.Append(row[i].ToString());
                }
                sb.AppendLine();
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=ventas.csv");
            Response.BinaryWrite(bytes);
            Response.End();
        }

        private DataTable VentasTabla()
        {
            var t = new DataTable();
            t.Columns.Add("Producto");
            t.Columns.Add("Categoria");
            t.Columns.Add("UnidadesVendidas", typeof(int));
            t.Columns.Add("PrecioUnitario", typeof(decimal));
            t.Columns.Add("Total", typeof(decimal));

            t.Rows.Add("Agua Purificada 20L", "Aguas", 2345, 25.00m, 58625.00m);
            t.Rows.Add("Bebida Isotónica Naranja 600ml", "Bebidas", 1102, 18.00m, 19836.00m);
            t.Rows.Add("Snack Mix Salado 50g", "Snacks", 876, 12.00m, 10512.00m);
            t.Rows.Add("Agua Mineral con Gas 1.5L", "Aguas", 650, 14.00m, 9100.00m);
            return t;
        }
    }
}
