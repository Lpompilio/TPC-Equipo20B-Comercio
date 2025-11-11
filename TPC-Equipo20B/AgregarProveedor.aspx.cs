using System;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        private readonly ProveedorNegocio _negocio = new ProveedorNegocio();
        private int Id => int.TryParse(Request.QueryString["id"], out var x) ? x : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Id != 0)
            {
                var p = _negocio.BuscarPorId(Id);
                if (p != null)
                {
                    txtNombre.Text = p.Nombre;
                    txtRazonSocial.Text = p.RazonSocial;
                    txtDocumento.Text = p.Documento;
                    txtIVA.Text = p.CondicionIVA;
                    txtEmail.Text = p.Email;
                    txtTelefono.Text = p.Telefono;
                    txtDireccion.Text = p.Direccion;
                    txtLocalidad.Text = p.Localidad;
                    lblTitulo.InnerText = "Editar Proveedor";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var p = new Proveedor
            {
                Id = Id,
                Nombre = txtNombre.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                Documento = txtDocumento.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                CondicionIVA = txtIVA.Text.Trim()
            };

            _negocio.Guardar(p);
            Response.Redirect("Proveedores.aspx");
        }
        }

        protected void btnCancelar_Click(object sender, EventArgs e) => Response.Redirect("Proveedores.aspx");
    }
}
