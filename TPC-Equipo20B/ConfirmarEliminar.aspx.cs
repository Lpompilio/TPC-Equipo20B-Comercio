using System;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class ConfirmarEliminar : System.Web.UI.Page
    {
        private string Entidad => (Request.QueryString["entidad"] ?? "").ToLowerInvariant();
        private int Id => int.TryParse(Request.QueryString["id"], out var x) ? x : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = Server.UrlDecode(Request.QueryString["msg"] ?? "¿Confirmar eliminación?");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Volver();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (Id == 0) { Volver(); return; }

            switch (Entidad)
            {
                case "producto":
                    new ProductoNegocio().Eliminar(Id);
                    Response.Redirect("Productos.aspx");
                    return;

                case "categoria":
                    new CategoriaNegocio().Eliminar(Id);
                    Response.Redirect("Categorias.aspx");
                    return;

                case "marca":
                    new MarcaNegocio().Eliminar(Id);
                    Response.Redirect("Marcas.aspx");
                    return;

                case "proveedor":
                    new ProveedorNegocio().Eliminar(Id);
                    Response.Redirect("Proveedores.aspx");
                    return;

                default:
                    Volver();
                    return;
            }
        }

        private void Volver()
        {
            Response.Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "Dashboard.aspx");
        }
    }
}
