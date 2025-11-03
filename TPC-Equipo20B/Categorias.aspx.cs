// UI/Categorias.aspx.cs
using System;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Categorias : System.Web.UI.Page
    {
        private readonly CategoriaNegocio _negocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) CargarGrid();
        }

        private void CargarGrid(string filtro = null)
        {
            gvCategorias.DataSource = _negocio.Listar(filtro);
            gvCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCategoria.aspx");
        }

        
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarGrid(txtBuscar.Text.Trim());
        }
    }
}
