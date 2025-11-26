using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo admin puede entrar
            if (Session["EsAdmin"] == null || !(bool)Session["EsAdmin"])
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind(string q = null)
        {
            UsuarioNegocio neg = new UsuarioNegocio();
            gvUsuarios.DataSource = neg.ListarUsuarios(q);
            gvUsuarios.DataBind();
        }

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            Bind(txtBuscarUsuario.Text.Trim());
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "HacerAdmin")
            {
                // Usuario actual desde la sesión
                int idActual = Convert.ToInt32(Session["UsuarioId"]);
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                // no permitir auto-modificarse
                if (idUsuario == idActual)
                    return;

                UsuarioNegocio neg = new UsuarioNegocio();
                neg.HacerAdmin(idUsuario);

                Bind();
            }
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario usuario = (Usuario)e.Row.DataItem;

                bool esAdmin = usuario.Roles.Any(r => r.Id == 1);

                LinkButton btn = (LinkButton)e.Row.FindControl("btnHacerAdmin");

                if (esAdmin)
                    btn.Visible = false; // si ya es admin, ocultamos el botón
            }
        }
    }


}