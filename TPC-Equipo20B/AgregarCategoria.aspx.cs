using System;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        private readonly CategoriaNegocio _negocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var nombre = (txtNombre.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
               
                return;
            }

            _negocio.Agregar(new Categoria { Nombre = nombre });
            Response.Redirect("Categorias.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorias.aspx");
        }
    }
}
