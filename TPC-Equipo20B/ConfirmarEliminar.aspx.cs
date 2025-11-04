using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class ConfirmarEliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string entidad = Request.QueryString["entidad"];
                string idStr = Request.QueryString["id"];

                if (string.IsNullOrEmpty(entidad) || string.IsNullOrEmpty(idStr))
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                ViewState["Entidad"] = entidad;
                ViewState["Id"] = int.Parse(idStr);

                lblMensaje.Text = $"¿Seguro que querés eliminar esta <strong>{entidad}</strong>?";
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string entidad = ViewState["Entidad"].ToString().ToLower();
            int id = (int)ViewState["Id"];

            switch (entidad)
            {
                case "producto":
                    new ProductoNegocio().Eliminar(id);
                    Response.Redirect("Productos.aspx");
                    break;

                case "marca":
                    new MarcaNegocio().Eliminar(id);
                    Response.Redirect("Marcas.aspx");
                    break;

                case "categoria":
                    new CategoriaNegocio().Eliminar(id);
                    Response.Redirect("Categorias.aspx");
                    break;

                case "proveedor":
                    new ProveedorNegocio().Eliminar(id);
                    Response.Redirect("Proveedores.aspx");
                    break;

                default:
                    Response.Redirect("Dashboard.aspx");
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string entidad = ViewState["Entidad"]?.ToString()?.ToLower();

            switch (entidad)
            {
                case "producto":
                    Response.Redirect("Productos.aspx");
                    break;

                case "marca":
                    Response.Redirect("Marcas.aspx");
                    break;

                case "categoria":
                    Response.Redirect("Categorias.aspx");
                    break;

                case "proveedor":
                    Response.Redirect("Proveedores.aspx");
                    break;

                default:
                    Response.Redirect("Dashboard.aspx");
                    break;
            }
        }
    }
}
