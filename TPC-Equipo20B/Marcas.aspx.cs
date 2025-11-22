using Negocio;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;


namespace TPC_Equipo20B
{
    public partial class Marcas : System.Web.UI.Page
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

        protected void gvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMarcas.PageIndex = e.NewPageIndex;
            BindGrid(txtBuscar.Text.Trim());
        }

        private void BindGrid(string q)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            var lista = negocio.Listar(q);

            gvMarcas.DataSource = lista.Select(m => new
            {
                m.Id,
                Nombre = m.Nombre.ToUpper()
            }).ToList();

            gvMarcas.DataBind();
        }

    }
}
