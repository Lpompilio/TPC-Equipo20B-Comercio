using Negocio;
using System;
using System.Web.UI.WebControls;

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
            Response.Redirect("AgregarProducto.aspx");
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("AgregarProducto.aspx?id=" + id);
                    break;

                case "Eliminar":
                    Response.Redirect("ConfirmarEliminar.aspx?entidad=producto&id=" + id);
                    break;
            }
        }

    }
}
