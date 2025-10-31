using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Pendiente: Guardar nueva categoría en la base.
            // Categoria nueva = new Categoria { Nombre = txtNombre.Text };
            // categoriaNegocio.Agregar(nueva);
            // Response.Redirect("Categorias.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorias.aspx");
        }
    }
}