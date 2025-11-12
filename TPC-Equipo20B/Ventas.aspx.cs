using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class Ventas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        // Ahora admite filtro opcional
        private void CargarGrid(string q = null)
        {
            var negocio = new VentaNegocio();
            List<Venta> lista = negocio.Listar(q);
            gvVentas.DataSource = lista;
            gvVentas.DataBind();
        }

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Detalle")
                Response.Redirect("VentaDetalle.aspx?id=" + id);
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarVenta.aspx");
        }

        protected void btnBuscarVenta_Click(object sender, EventArgs e)
        {
            var q = txtBuscarVenta.Text ?? string.Empty;
            CargarGrid(q.Trim());
        }
    }
}
