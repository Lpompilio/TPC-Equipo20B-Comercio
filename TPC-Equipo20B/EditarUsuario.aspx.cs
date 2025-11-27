using System;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo admin
            if (Session["EsAdmin"] == null || !(bool)Session["EsAdmin"])
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("Usuarios.aspx", false);
                    return;
                }

                int idUsuario;
                if (!int.TryParse(Request.QueryString["id"], out idUsuario))
                {
                    Response.Redirect("Usuarios.aspx", false);
                    return;
                }

                ViewState["IdUsuario"] = idUsuario;
                CargarUsuario(idUsuario);
            }
        }

        private void CargarUsuario(int idUsuario)
        {
            UsuarioNegocio neg = new UsuarioNegocio();
            Usuario u = neg.ObtenerUsuarioPorId(idUsuario);

            if (u == null)
            {
                Response.Redirect("Usuarios.aspx", false);
                return;
            }

            txtNombre.Text = u.Nombre;
            txtEmail.Text = u.Email;
            txtDocumento.Text = u.Documento;
            txtTelefono.Text = u.Telefono;
            txtDireccion.Text = u.Direccion;
            txtLocalidad.Text = u.Localidad;
            txtUsername.Text = u.Username;

            lblEstado.Text = u.Activo ? "Activo" : "Deshabilitado";
            lblEstado.CssClass = u.Activo ? "badge bg-success" : "badge bg-secondary";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (ViewState["IdUsuario"] == null)
            {
                Response.Redirect("Usuarios.aspx", false);
                return;
            }

            int idUsuario = (int)ViewState["IdUsuario"];

            Usuario u = new Usuario
            {
                Id = idUsuario,
                Nombre = txtNombre.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Documento = string.IsNullOrWhiteSpace(txtDocumento.Text) ? null : txtDocumento.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                Localidad = string.IsNullOrWhiteSpace(txtLocalidad.Text) ? null : txtLocalidad.Text.Trim()
            };

            UsuarioNegocio neg = new UsuarioNegocio();
            neg.ActualizarUsuario(u);

            // Podés redirigir directo o mostrar mensaje
            Response.Redirect("Usuarios.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }
    }
}
