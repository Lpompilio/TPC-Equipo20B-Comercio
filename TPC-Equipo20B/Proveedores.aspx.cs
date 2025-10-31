using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            // Pendiente: Cargar gvProveedores con lista de proveedores desde la base de datos.
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProveedor.aspx");
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            // Pendiente: Filtrar gvProveedores según el texto en txtBuscarProveedor.Text.
        }
    }
}