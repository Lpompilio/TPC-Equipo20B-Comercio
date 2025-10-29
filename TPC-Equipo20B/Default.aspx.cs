using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
            }
        }

        private void cargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.Listar();

            gvProductos.DataSource = lista;
            gvProductos.DataBind();
        }
    }
}