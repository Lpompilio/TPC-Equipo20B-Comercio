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
            if (string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;

            int idActual = Convert.ToInt32(Session["UsuarioId"]);
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            // No permitir auto-modificarse
            if (idUsuario == idActual)
                return;

            UsuarioNegocio neg = new UsuarioNegocio();

            switch (e.CommandName)
            {
                case "CambiarRol":
                    Usuario u = neg.ObtenerUsuarioPorId(idUsuario);
                    bool esAdmin = u.Roles.Any(r => r.Id == 1);

                    if (esAdmin)
                        neg.HacerVendedor(idUsuario);
                    else
                        neg.HacerAdmin(idUsuario);
                    break;

                case "EditarUsuario":
                    Response.Redirect("EditarUsuario.aspx?id=" + idUsuario, false);
                    return;

                case "ToggleActivo":
                    Usuario usuario = neg.ObtenerUsuarioPorId(idUsuario);

                    // por seguridad: no permitir deshabilitar admins desde acá
                    if (usuario.Roles.Any(r => r.Id == 1))
                        return;

                    if (usuario.Activo)
                        neg.DeshabilitarUsuario(idUsuario);
                    else
                        neg.HabilitarUsuario(idUsuario);
                    break;
            }

            // Volvemos a bindear respetando el filtro actual
            Bind(txtBuscarUsuario.Text.Trim());
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Usuario usuario = (Usuario)e.Row.DataItem;
            int idActual = Convert.ToInt32(Session["UsuarioId"]);

            LinkButton btnCambiarRol = (LinkButton)e.Row.FindControl("btnCambiarRol");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");
            LinkButton btnToggleActivo = (LinkButton)e.Row.FindControl("btnToggleActivo");

            bool esAdmin = usuario.Roles.Any(r => r.Id == 1);

            // Si es el usuario logueado → ocultamos todas las acciones
            if (usuario.Id == idActual)
            {
                btnCambiarRol.Visible = false;
                btnEditar.Visible = false;
                btnToggleActivo.Visible = false;
                return;
            }

            // ---- Botón Cambiar Rol ----
            if (esAdmin)
            {
                btnCambiarRol.Text = "Hacer Vendedor";
                btnCambiarRol.CssClass = "btn btn-sm btn-user-action btn-role-vendedor";
            }
            else
            {
                btnCambiarRol.Text = "Hacer Admin";
                btnCambiarRol.CssClass = "btn btn-sm btn-user-action btn-role-admin";
            }

            // ---- Botón Editar (siempre visible, ya tiene su clase en el .aspx) ----
            btnEditar.Visible = true;

            // ---- Botón Activar / Desactivar ----
            if (usuario.Activo)
            {
                btnToggleActivo.Text = "Deshabilitar";
                btnToggleActivo.CssClass = "btn btn-sm btn-user-action btn-disable";
            }
            else
            {
                btnToggleActivo.Text = "Habilitar";
                btnToggleActivo.CssClass = "btn btn-sm btn-user-action btn-enable";
            }
        }
    }
}
