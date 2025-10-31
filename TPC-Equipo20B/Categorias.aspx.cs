using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void CargarCategorias()
        {
            // Pendiente: Cargar gvCategorias con la lista de categorías desde la base.
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCategoria.aspx");
        }
    }
}