using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Categorias : System.Web.UI.Page
    {
        private string cn => ConfigurationManager.ConnectionStrings["COMERCIO_DB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid("");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BindGrid(txtBuscar.Text.Trim());
        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorias.PageIndex = e.NewPageIndex;
            BindGrid(txtBuscar.Text.Trim());
        }

        private void BindGrid(string q)
        {
            using (var con = new SqlConnection(cn))
            using (var cmd = new SqlCommand(@"
        SELECT Id, UPPER(Nombre) AS Nombre
        FROM CATEGORIAS
        WHERE (@q = '' OR Nombre LIKE '%'+@q+'%')
        ORDER BY Nombre;", con))
            {
                cmd.Parameters.Add("@q", SqlDbType.VarChar, 100).Value = q ?? "";
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                gvCategorias.DataSource = dt;
                gvCategorias.DataBind();
            }
        }
    }
}
