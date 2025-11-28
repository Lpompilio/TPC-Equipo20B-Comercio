using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
            int idUsuarioLogueado = Session["UsuarioId"] != null
                ? (int)Session["UsuarioId"]
                : -1;

            if (!esAdmin)
            {
                // Solo ventas generadas por el usuario logueado
                lista = lista
                    .Where(v => v.Usuario != null && v.Usuario.Id == idUsuarioLogueado)
                    .ToList();
            }

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
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Venta venta = (Venta)e.Row.DataItem;

            LinkButton btnCancelar = (LinkButton)e.Row.FindControl("cmdCancelar");

            if (btnCancelar == null)
                return; // 

            // Si ya está cancelada, no mostrar el botón
            if (venta.Cancelada)
            {
                btnCancelar.Visible = false;
                return;
            }

            // Obtener info de sesión
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];

            int idUsuarioLogueado = Session["UsuarioId"] != null
                ? (int)Session["UsuarioId"]
                : -1;

            // Vendedor solo cancelar sus ventas
            if (!esAdmin)
            {
                int idVendedorDeLaVenta = venta.Usuario != null ? venta.Usuario.Id : -1;

                if (idVendedorDeLaVenta != idUsuarioLogueado)
                {
                    btnCancelar.Visible = false;
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
