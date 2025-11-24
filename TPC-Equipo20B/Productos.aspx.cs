using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Productos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                Bind();
            }
        }

        private void CargarProveedores()
        {
            var negocio = new ProveedorNegocio();
            ddlProveedor.DataSource = negocio.Listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Todos los proveedores", "0"));
        }

        private void Bind(string q = null, int? idProveedor = null)
        {
            var negocio = new ProductoNegocio();
            gvProductos.DataSource = negocio.Listar(q, idProveedor);
            gvProductos.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProducto.aspx");
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            var q = txtBuscarProducto.Text.Trim();

            int idProv;
            if (int.TryParse(ddlProveedor.SelectedValue, out idProv) && idProv > 0)
                Bind(q, idProv);
            else
                Bind(q, null);
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = txtBuscarProducto.Text.Trim();

            int idProv;
            if (int.TryParse(ddlProveedor.SelectedValue, out idProv) && idProv > 0)
                Bind(q, idProv);
            else
                Bind(q, null);
        }

        protected void gvProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
                Response.Redirect("AgregarProducto.aspx?id=" + e.CommandArgument);

            if (e.CommandName == "Eliminar")
                Response.Redirect("ConfirmarEliminar.aspx?tipo=producto&id=" + e.CommandArgument);
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow ||
                e.Row.RowType == DataControlRowType.Header)
            {
                bool esAdmin = (bool)(Session["EsAdmin"] ?? false);

                int indexAcciones = gvProductos.Columns.Count - 1;

                e.Row.Cells[indexAcciones].Visible = esAdmin;
            }
        }

    }
}
