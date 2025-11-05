using System;
using System.Linq;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;

namespace TPC_Equipo20B
{
    public partial class Productos : System.Web.UI.Page
    {
        private readonly ProductoNegocio _negocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            List<Producto> lista = _negocio.Listar();
            gvProductos.DataSource = lista;
            gvProductos.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoEditar.aspx");
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                var id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ProductoEditar.aspx?id=" + id);
                return;
            }

            if (e.CommandName == "Eliminar")
            {
                var id = Convert.ToInt32(e.CommandArgument);
               
                string msg = "¿Desea eliminar el producto seleccionado?";
                Response.Redirect($"ConfirmarEliminar.aspx?entidad=producto&id={id}&msg={Server.UrlEncode(msg)}");
            }
        }
    }
}
