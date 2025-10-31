using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class AgregarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Pendiente: Guardar nueva marca en la base.
            // Marca nueva = new Marca { Nombre = txtNombre.Text };
            // marcaNegocio.Agregar(nueva);
            // Response.Redirect("Marcas.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marcas.aspx");
        }
    }
}