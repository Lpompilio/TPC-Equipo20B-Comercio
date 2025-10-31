using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            //    CargarClientes();
            }
        }

        private void CargarClientes()
        {
            // Pendiente: Cargar gvClientes con lista de clientes desde la base de datos.
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCliente.aspx");
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            // Pendiente: Filtrar gvClientes según el texto en txtBuscarCliente.Text.
        }
    }
}