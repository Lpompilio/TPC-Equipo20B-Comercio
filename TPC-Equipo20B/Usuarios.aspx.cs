using Dominio;
using Negocio;
using System;
using System.Linq;
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
            if (e.CommandName == "HacerAdmin" || e.CommandName == "HacerVendedor")
            {
                int idActual = Convert.ToInt32(Session["UsuarioId"]);
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                // Seguridad extra: no permitir auto-modificarse
                if (idUsuario == idActual)
                    return;

                UsuarioNegocio neg = new UsuarioNegocio();

                if (e.CommandName == "HacerAdmin")
                    neg.HacerAdmin(idUsuario);
                else
                    neg.HacerVendedor(idUsuario);

                // Re-bind con el filtro actual (si hubiera)
                Bind(txtBuscarUsuario.Text.Trim());
            }
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Usuario usuario = (Usuario)e.Row.DataItem;
            LinkButton btn = (LinkButton)e.Row.FindControl("btnHacerAdmin");
            if (btn == null)
                return;

            int idActual = Convert.ToInt32(Session["UsuarioId"]);

            // ✅ Opción A: en la fila del usuario logueado NO se muestra el botón
            if (usuario.Id == idActual)
            {
                btn.Visible = false;
                return;
            }

            bool esAdmin = usuario.Roles.Any(r => r.Id == 1);

            if (esAdmin)
            {
                // Ya es admin → permitir pasarlo a vendedor
                btn.Text = "Hacer Vendedor";
                btn.CommandName = "HacerVendedor";
                btn.CssClass = "btn btn-outline-secondary btn-action-sm";
            }
            else
            {
                // Es vendedor → permitir hacerlo admin
                btn.Text = "Hacer Admin";
                btn.CommandName = "HacerAdmin";
                btn.CssClass = "btn btn-warning btn-action-sm";
            }
        }
    }
}
