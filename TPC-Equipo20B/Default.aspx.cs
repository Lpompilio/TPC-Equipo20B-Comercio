using System;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            // Si ya está logueado, redirigir al dashboard
            if (!IsPostBack && Session["Usuario"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                // Validar credenciales
                Usuario usuario = negocio.ValidarLogin(username, password);

                if (usuario != null)
                {
                    // Guardar usuario en sesión
                    Session["Usuario"] = usuario;
                    Session["UsuarioId"] = usuario.Id;
                    Session["UsuarioNombre"] = usuario.Nombre;
                    Session["Username"] = usuario.Username;

                    // Guardar roles en sesión (útil para verificar permisos)
                    Session["Roles"] = usuario.Roles;

                    // Verificar si es Admin
                    bool esAdmin = usuario.Roles.Exists(r => r.Nombre == "Admin");
                    Session["EsAdmin"] = esAdmin;

                    lblError.Text = "";

                    // Redirigir al dashboard
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al iniciar sesión: " + ex.Message;
            }
        }
    }
}