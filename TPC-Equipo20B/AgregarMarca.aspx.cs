using System;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class AgregarMarca : System.Web.UI.Page
    {
        private readonly MarcaNegocio _negocio = new MarcaNegocio();
        private int Id => int.TryParse(Request.QueryString["id"], out var x) ? x : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Id != 0)
            {
                lblTitulo.InnerText = "Editar Marca";
                var lista = _negocio.Listar();
                var m = lista.Find(x => x.Id == Id);
                if (m != null) txtNombre.Text = m.Nombre;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var m = new Dominio.Marca { Id = Id, Nombre = txtNombre.Text.Trim() };
            if (m.Id == 0) _negocio.Agregar(m); else _negocio.Modificar(m);
            Response.Redirect("Marcas.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e) => Response.Redirect("Marcas.aspx");
    }
}
