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
    public partial class AgregarMarca : System.Web.UI.Page
    {
        private int idMarca = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifico si vengo con un ID 
                if (Request.QueryString["id"] != null)
                {
                    idMarca = int.Parse(Request.QueryString["id"]);
                    CargarMarca(idMarca);
                    // Cambio título y texto del botón
                    lblTitulo.InnerText = "Editar Marca";
                    btnGuardar.Text = "Guardar Cambios";
                }
            }
        }

        private void CargarMarca(int id)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            var lista = negocio.Listar();
            Marca marca = lista.Find(m => m.Id == id);

            if (marca != null)
            {
                txtNombre.Text = marca.Nombre;
                ViewState["idMarca"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca
            {
                Nombre = txtNombre.Text.Trim()
            };

            // Si hay id almacenado es modificación
            if (ViewState["idMarca"] != null)
            {
                marca.Id = (int)ViewState["idMarca"];
                negocio.Modificar(marca);
            }
            else
            {
                negocio.Agregar(marca);
            }

            Response.Redirect("Marcas.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marcas.aspx", false);
        }
    }
}