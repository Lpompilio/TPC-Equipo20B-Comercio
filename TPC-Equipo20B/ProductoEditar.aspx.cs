using System;
using Negocio;
using Dominio;

namespace TPC_Equipo20B
{
    public partial class ProductoEditar : System.Web.UI.Page
    {
        private readonly ProductoNegocio _negocio = new ProductoNegocio();

        private int IdProducto
        {
            get
            {
                int id;
                return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litTitulo.Text = IdProducto == 0 ? "Agregar Nuevo Producto" : "Editar Producto";
                if (IdProducto != 0) CargarProducto(IdProducto);
            }
        }

        private void CargarProducto(int id)
        {
            var p = _negocio.ObtenerPorId(id);
            if (p == null) return;

            txtDescripcion.Text = p.Descripcion;
            txtCodigo.Text = p.CodigoSKU;
            txtStockMinimo.Text = p.StockMinimo.ToString("0.##");
            txtStockActual.Text = p.StockActual.ToString("0.##");
            txtGanancia.Text = p.PorcentajeGanancia.ToString("0.##");
            txtUrlImagen.Text = p.UrlImagen ?? "";
            chkActivo.Checked = p.Activo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var p = new Producto
            {
                Id = IdProducto,
                Descripcion = txtDescripcion.Text?.Trim(),
                CodigoSKU = txtCodigo.Text?.Trim(),
                UrlImagen = txtUrlImagen.Text?.Trim(),
                Activo = chkActivo.Checked
            };

            decimal tmp;
            if (decimal.TryParse(txtStockMinimo.Text, out tmp)) p.StockMinimo = tmp; else p.StockMinimo = 0;
            if (decimal.TryParse(txtStockActual.Text, out tmp)) p.StockActual = tmp; else p.StockActual = 0;
            if (decimal.TryParse(txtGanancia.Text, out tmp)) p.PorcentajeGanancia = tmp; else p.PorcentajeGanancia = 0;

            _negocio.Guardar(p);
            Response.Redirect("Productos.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }
    }
}
