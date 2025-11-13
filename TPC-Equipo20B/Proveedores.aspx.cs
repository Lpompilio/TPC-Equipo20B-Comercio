using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Proveedores : System.Web.UI.Page
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

        protected void gvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProveedores.PageIndex = e.NewPageIndex;
            BindGrid(txtBuscar.Text.Trim());
        }

        private void BindGrid(string q)
        {
            using (var con = new SqlConnection(cn))
            using (var cmd = new SqlCommand(@"
                SELECT 
            Id,
            -- ESTA ES LA LÍNEA CLAVE:
            -- Si 'Nombre' (contacto) existe, muestra 'Nombre (RazonSocial)'.
            -- Si 'Nombre' es NULO, muestra solo 'RazonSocial'.
            UPPER(COALESCE(Nombre + ' (' + RazonSocial + ')', RazonSocial)) AS NombreCompleto,
            
            COALESCE(Documento,'') AS Documento,
            COALESCE(Telefono,'')  AS Telefono
        FROM dbo.PROVEEDORES
        WHERE (
            @q = '' OR
            -- Modificamos la búsqueda para que busque en AMBOS campos
            Nombre LIKE '%'+@q+'%' OR
            RazonSocial LIKE '%'+@q+'%' OR
            COALESCE(Documento,'') LIKE '%'+@q+'%'
        )
        -- Ordenamos por Razón Social (el nombre principal)
        ORDER BY UPPER(RazonSocial), UPPER(Nombre);", con))
            {
                cmd.Parameters.Add("@q", SqlDbType.VarChar, 100).Value = q ?? "";
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                gvProveedores.DataSource = dt;
                gvProveedores.DataBind();
            }
        }
    }
}
