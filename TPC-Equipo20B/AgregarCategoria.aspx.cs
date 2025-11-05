using System;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        private readonly CategoriaNegocio _negocio = new CategoriaNegocio();
        private int Id => int.TryParse(Request.QueryString["id"], out var x) ? x : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Id != 0)
            {
                lblTitulo.InnerText = "Editar Categoría";
                var cat = _negocio.ObtenerPorId(Id);
                if (cat != null) txtNombre.Text = cat.Nombre;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var cat = new Categoria { Id = Id, Nombre = txtNombre.Text.Trim() };
            if (cat.Id == 0) _negocio.Agregar(cat); else _negocio.Modificar(cat);
            Response.Redirect("Categorias.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e) => Response.Redirect("Categorias.aspx");
    }
}
