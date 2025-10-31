using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void CargarMarcas()
        {
            // Pendiente: Cargar gvMarcas con la lista de marcas desde la base.
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMarca.aspx");
        }
    }
}