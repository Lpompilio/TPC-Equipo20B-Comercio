using System;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }

        // Métodos de navegación existentes
        protected void lnkDashboard_Click(object sender, EventArgs e) => Response.Redirect("~/Dashboard.aspx", false);
        protected void lnkProductos_Click(object sender, EventArgs e) => Response.Redirect("~/Productos.aspx", false);
        protected void lnkVentas_Click(object sender, EventArgs e) => Response.Redirect("~/Ventas.aspx", false);
        protected void lnkReportes_Click(object sender, EventArgs e) => Response.Redirect("~/Reportes.aspx", false);
        protected void lnkCategorias_Click(object sender, EventArgs e) => Response.Redirect("~/Categorias.aspx", false);
        protected void lnkMarcas_Click(object sender, EventArgs e) => Response.Redirect("~/Marcas.aspx", false);
        protected void lnkProveedores_Click(object sender, EventArgs e) => Response.Redirect("~/Proveedores.aspx", false);
        protected void lnkClientes_Click(object sender, EventArgs e) => Response.Redirect("~/Clientes.aspx", false);
    }
}
