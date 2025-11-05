using System;

namespace TPC_Equipo20B
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void lnkDashboard_Click(object sender, EventArgs e) => Response.Redirect("Dashboard.aspx");
        protected void lnkProductos_Click(object sender, EventArgs e) => Response.Redirect("Productos.aspx");
        protected void lnkVentas_Click(object sender, EventArgs e) => Response.Redirect("Ventas.aspx");

        
        protected void lnkReportes_Click(object sender, EventArgs e) => Response.Redirect("Reportes.aspx");

        protected void lnkCategorias_Click(object sender, EventArgs e) => Response.Redirect("Categorias.aspx");
        protected void lnkMarcas_Click(object sender, EventArgs e) => Response.Redirect("Marcas.aspx");
        protected void lnkProveedores_Click(object sender, EventArgs e) => Response.Redirect("Proveedores.aspx");
        protected void lnkClientes_Click(object sender, EventArgs e) => Response.Redirect("Clientes.aspx");
    }
}
