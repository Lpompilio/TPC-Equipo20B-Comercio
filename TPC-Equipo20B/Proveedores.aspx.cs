using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Proveedores : System.Web.UI.Page
    {
        private readonly ProveedorNegocio _negocio = new ProveedorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            gvProveedores.DataSource = _negocio.Listar();
            gvProveedores.DataBind();
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProveedor.aspx");
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect("AgregarProveedor.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                string msg = "¿Desea eliminar el proveedor?";
                Response.Redirect($"ConfirmarEliminar.aspx?entidad=proveedor&id={id}&msg={Server.UrlEncode(msg)}");
            }
        }
    }
}
