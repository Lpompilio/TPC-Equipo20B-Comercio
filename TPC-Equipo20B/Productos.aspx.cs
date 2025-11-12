using System;
using System.Web.UI;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Productos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) Bind();
        }

        private void Bind(string q = null)
        {
            var negocio = new ProductoNegocio();
            gvProductos.DataSource = negocio.Listar(q);
            gvProductos.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoEditar.aspx");
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            var q = Request.Form["txtBuscarProducto"];
            Bind((q ?? "").Trim());
        }

        protected void gvProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
                Response.Redirect("AgregarProducto.aspx?id=" + e.CommandArgument);
            if (e.CommandName == "Eliminar")
                Response.Redirect("ConfirmarEliminar.aspx?tipo=producto&id=" + e.CommandArgument);
        }
    }
}
