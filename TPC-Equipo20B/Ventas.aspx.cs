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

            else if (e.CommandName == "Cancelar")
            {
                string msg = Server.UrlEncode(
                    $"¿Cancelar la venta #{id}? Se reintegrará el stock."
                );
                Response.Redirect($"ConfirmarEliminar.aspx?tipo=venta&id={id}&msg={msg}");
            }
        }

        protected void gvVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Venta venta = (Venta)e.Row.DataItem;

                LinkButton btnCancelar = (LinkButton)e.Row.FindControl("cmdCancelar");

                if (venta.Cancelada)
                {
                    btnCancelar.Visible = false;
                }
                else
                {
                    btnCancelar.Visible = true;
                }
            }
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
