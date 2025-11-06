using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> lista = negocio.Listar();
            gvVentas.DataSource = lista;
            gvVentas.DataBind();
        }

        protected void gvVentas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
    }
}
