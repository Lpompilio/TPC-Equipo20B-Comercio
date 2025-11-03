using System;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class ProductoEditar : System.Web.UI.Page
    {
        private int? IdProducto =>
            int.TryParse(Request.QueryString["id"], out var id) ? id : (int?)null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litTitulo.Text = IdProducto.HasValue ? "Editar Producto" : "Agregar Nuevo Producto";
                if (IdProducto.HasValue)
                    CargarProducto(IdProducto.Value);
            }
        }

        private void CargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto p = negocio.ObtenerPorId(id);

            if (p == null) return;

            txtDescripcion.Text = p.Descripcion;
            txtCodigo.Text = p.CodigoSKU;
            txtStockMinimo.Text = p.StockMinimo.ToString();
            txtStockActual.Text = p.StockActual.ToString();
            txtGanancia.Text = p.PorcentajeGanancia.ToString();
            txtUrlImagen.Text = p.UrlImagen;
            chkActivo.Checked = p.Activo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto
            {
                Id = IdProducto ?? 0,
                Descripcion = txtDescripcion.Text,
                CodigoSKU = txtCodigo.Text,
                StockMinimo = decimal.Parse(txtStockMinimo.Text),
                StockActual = decimal.Parse(txtStockActual.Text),
                PorcentajeGanancia = decimal.Parse(txtGanancia.Text),
                UrlImagen = txtUrlImagen.Text,
                Activo = chkActivo.Checked
            };

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.Guardar(p);
            Response.Redirect("Productos.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }
    }
}
