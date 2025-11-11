using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            CompraNegocio negocio = new CompraNegocio();
            List<Compra> lista = negocio.Listar();
            gvCompras.DataSource = lista;
            gvCompras.DataBind();
        }

        protected void gvCompras_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Detalle")
                Response.Redirect("CompraDetalle.aspx?id=" + id);

            else if (e.CommandName == "Eliminar")
            {
                string msg = Server.UrlEncode($"¿Desea eliminar la compra N° {id}? Esta acción no se puede deshacer.");
                Response.Redirect($"ConfirmarEliminar.aspx?entidad=compra&id={id}&msg={msg}");
            }
        }

        protected void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCompra.aspx");
        }
    }
}
