using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Marcas : System.Web.UI.Page
    {
        private readonly MarcaNegocio _negocio = new MarcaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            gvMarcas.DataSource = _negocio.Listar();
            gvMarcas.DataBind();
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMarca.aspx");
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect("AgregarMarca.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                string msg = "¿Desea eliminar la marca?";
                Response.Redirect($"ConfirmarEliminar.aspx?entidad=marca&id={id}&msg={Server.UrlEncode(msg)}");
            }
        }
    }
}
