using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargarProductos()
        {
            // Pendiente: Cargar gvProductos con lista de productos.
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProducto.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Pendiente: aplicar filtros según los valores seleccionados
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Pendiente: lógica para editar o eliminar
        }
    }

}