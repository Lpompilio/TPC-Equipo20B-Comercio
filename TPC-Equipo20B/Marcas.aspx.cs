using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo20B
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarMarcas();
        }
        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            gvMarcas.DataSource = negocio.Listar();
            gvMarcas.DataBind();
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMarca.aspx");
        }
        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("AgregarMarca.aspx?id=" + id);
                    break;

                case "Eliminar":
                    Response.Redirect("ConfirmarEliminar.aspx?entidad=marca&id=" + id);
                    break;
            }
        }
    }
}