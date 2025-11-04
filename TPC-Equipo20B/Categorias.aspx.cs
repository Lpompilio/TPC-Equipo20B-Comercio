// UI/Categorias.aspx.cs
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            gvCategorias.DataSource = negocio.Listar();
            gvCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCategoria.aspx");
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("AgregarCategoria.aspx?id=" + id);
                    break;

                case "Eliminar":
                    Response.Redirect("ConfirmarEliminar.aspx?entidad=categoria&id=" + id);
                    break;
            }
        }
    }
}
