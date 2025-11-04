using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        private int idProveedor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Modo edición si hay id 
                if (Request.QueryString["id"] != null)
                {
                    idProveedor = int.Parse(Request.QueryString["id"]);
                    CargarProveedor(idProveedor);
                    lblTitulo.InnerText = "Editar Proveedor";
                    btnGuardar.Text = "Guardar Cambios";
                }
            }
        }

        private void CargarProveedor(int id)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor prov = negocio.BuscarPorId(id);

            if (prov != null)
            {
                txtNombre.Text = prov.Nombre;
                txtRazonSocial.Text = prov.RazonSocial;
                txtDocumento.Text = prov.Documento;
                txtEmail.Text = prov.Email;
                txtTelefono.Text = prov.Telefono;
                txtDireccion.Text = prov.Direccion;
                txtLocalidad.Text = prov.Localidad;
                txtIVA.Text = prov.CondicionIVA;

                ViewState["idProveedor"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor p = new Proveedor
            {
                Nombre = txtNombre.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                Documento = txtDocumento.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                CondicionIVA = txtIVA.Text.Trim()
            };

            // Si estamos editando
            if (ViewState["idProveedor"] != null)
                p.Id = (int)ViewState["idProveedor"];

            negocio.Guardar(p);

            Response.Redirect("Proveedores.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx", false);
        }
    }
}
