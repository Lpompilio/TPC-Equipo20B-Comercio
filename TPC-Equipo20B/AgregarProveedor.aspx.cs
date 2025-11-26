using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

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

        protected void cvNombreRazon_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool tieneNombre = !string.IsNullOrEmpty(txtNombre.Text);
            bool tieneRazon = !string.IsNullOrEmpty(txtRazonSocial.Text);

            // Si tiene nombre O tiene razón social, es válido.
            if (tieneNombre || tieneRazon)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            try
            {
                Proveedor p = new Proveedor
                {
                    Id = (Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0),
                    Nombre = txtNombre.Text,
                    RazonSocial = txtRazonSocial.Text,
                    Documento = txtDocumento.Text,
                    Email = txtEmail.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text,
                    Localidad = txtLocalidad.Text,
                    CondicionIVA = txtIVA.Text
                };

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.Guardar(p);

                Response.Redirect("Proveedores.aspx", false);
            }
            catch (Exception ex)
            {
                panelError.Visible = true;
                lblError.Text = ex.Message;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx");
        }
    }
}
