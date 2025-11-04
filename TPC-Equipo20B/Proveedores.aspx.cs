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
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            gvProveedores.DataSource = negocio.Listar();
            gvProveedores.DataBind();
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProveedor.aspx");
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("AgregarProveedor.aspx?id=" + id);
                    break;

                case "Eliminar":
                    Response.Redirect("ConfirmarEliminar.aspx?entidad=proveedor&id=" + id);
                    break;
            }
        }
    }
}
