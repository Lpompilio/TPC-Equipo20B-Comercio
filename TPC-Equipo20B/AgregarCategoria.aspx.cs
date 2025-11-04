using System;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        private int idCategoria = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si vengo en modo editar
                if (Request.QueryString["id"] != null)
                {
                    idCategoria = int.Parse(Request.QueryString["id"]);
                    CargarCategoria(idCategoria);
                    lblTitulo.InnerText = "Editar Categoría";
                    btnGuardar.Text = "Guardar Cambios";
                }
            }
        }

        private void CargarCategoria(int id)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            var lista = negocio.Listar();
            Categoria categoria = lista.Find(c => c.Id == id);

            if (categoria != null)
            {
                txtNombre.Text = categoria.Nombre;
                ViewState["idCategoria"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria
            {
                Nombre = txtNombre.Text.Trim()
            };

            // Si hay ID, modificar, si no, agregar
            if (ViewState["idCategoria"] != null)
            {
                categoria.Id = (int)ViewState["idCategoria"];
                negocio.Modificar(categoria);
            }
            else
            {
                negocio.Agregar(categoria);
            }

            Response.Redirect("Categorias.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorias.aspx", false);
        }
    }
}
