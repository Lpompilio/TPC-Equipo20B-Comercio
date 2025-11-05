using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class Categorias : System.Web.UI.Page
    {
        private readonly CategoriaNegocio _negocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid(string filtro = null)
        {
            gvCategorias.DataSource = _negocio.Listar(filtro);
            gvCategorias.DataBind();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCategoria.aspx");
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect("AgregarCategoria.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                string msg = "¿Desea eliminar la categoría?";
                Response.Redirect($"ConfirmarEliminar.aspx?entidad=categoria&id={id}&msg={Server.UrlEncode(msg)}");
            }
        }
    }
}
