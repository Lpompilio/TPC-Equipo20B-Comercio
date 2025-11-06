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
    public partial class AgregarCliente : System.Web.UI.Page
    {
        private int idCliente = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si hay un id entonces es edición
                if (Request.QueryString["id"] != null)
                {
                    idCliente = int.Parse(Request.QueryString["id"]);
                    CargarCliente(idCliente);
                    lblTitulo.InnerText = "Editar Cliente";
                    btnGuardar.Text = "Guardar Cambios";
                }
            }
        }

        private void CargarCliente(int id)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente c = negocio.BuscarPorId(id);

            if (c != null)
            {
                txtNombre.Text = c.Nombre;
                txtDocumento.Text = c.Documento;
                txtEmail.Text = c.Email;
                txtTelefono.Text = c.Telefono;
                txtDireccion.Text = c.Direccion;
                txtLocalidad.Text = c.Localidad;
                ddlCondicionIVA.SelectedValue = c.CondicionIVA;
                chkHabilitado.Checked = c.Habilitado;

                ViewState["idCliente"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente c = new Cliente
            {
                Nombre = txtNombre.Text.Trim(),
                Documento = txtDocumento.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                CondicionIVA = ddlCondicionIVA.SelectedValue,
                Habilitado = chkHabilitado.Checked
            };

            // Si es edición, guaro el id
            if (ViewState["idCliente"] != null)
                c.Id = (int)ViewState["idCliente"];

            negocio.Guardar(c);

            Response.Redirect("Clientes.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx", false);
        }
    }
}
