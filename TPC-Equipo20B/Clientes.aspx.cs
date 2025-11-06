using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class Clientes : System.Web.UI.Page
    {
        private ClienteNegocio negocio = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid(string filtro = "")
        {
            var lista = negocio.Listar();

            // Si hay texto de búsqueda, filtro por nombre o documento
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower();
                lista = lista.Where(c =>
                    (c.Nombre != null && c.Nombre.ToLower().Contains(filtro)) ||
                    (c.Documento != null && c.Documento.ToLower().Contains(filtro))
                ).ToList();
            }

            gvClientes.DataSource = lista;
            gvClientes.DataBind();
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCliente.aspx");
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscarCliente.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvClientes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("AgregarCliente.aspx?id=" + id);
                    break;

                case "Eliminar":
                    Response.Redirect("ConfirmarEliminar.aspx?entidad=cliente&id=" + id);
                    break;
            }
        }
    }
}
