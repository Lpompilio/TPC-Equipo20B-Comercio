using System;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            gvProductos.DataSource = negocio.Listar();
            gvProductos.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoEditar.aspx");
        }

        protected void gvProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
                Response.Redirect("ProductoEditar.aspx?id=" + id);
            else if (e.CommandName == "Eliminar")
            {
                ProductoNegocio negocio = new ProductoNegocio();
                negocio.Eliminar(id);
                CargarGrid();
            }
        }
    }
}
