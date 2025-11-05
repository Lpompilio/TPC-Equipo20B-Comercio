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
    public partial class AgregarProducto : System.Web.UI.Page
    {
        private int idProducto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();

                // Si viene con id es modo edición
                if (Request.QueryString["id"] != null)
                {
                    idProducto = int.Parse(Request.QueryString["id"]);
                    CargarProducto(idProducto);
                    lblTitulo.InnerText = "Editar Producto";
                    btnGuardar.Text = "Guardar Cambios";
                }
            }
        }

        private void CargarCombos()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            ddlMarca.DataSource = marcaNegocio.Listar();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlCategoria.DataSource = categoriaNegocio.Listar();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            ddlProveedor.Items.Clear();
            ddlProveedor.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione un proveedor...", "0"));
        }

        private void CargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.ObtenerPorId(id);

            if (producto != null)
            {
                txtDescripcion.Text = producto.Descripcion;
                txtCodigo.Text = producto.CodigoSKU;
                txtStockMinimo.Text = producto.StockMinimo.ToString();
                txtStockActual.Text = producto.StockActual.ToString();
                txtGanancia.Text = producto.PorcentajeGanancia.ToString();
                txtUrlImagen.Text = producto.UrlImagen;
                chkActivo.Checked = producto.Activo;

                if (producto.Marca != null)
                    ddlMarca.SelectedValue = producto.Marca.Id.ToString();

                if (producto.Categoria != null)
                    ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();

                ViewState["idProducto"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            int stockMin = 0;
            int stockAct = 0;
            decimal ganancia = 0;

            int.TryParse(txtStockMinimo.Text, out stockMin);
            int.TryParse(txtStockActual.Text, out stockAct);
            decimal.TryParse(txtGanancia.Text, out ganancia);

            int idMarca = 0;
            int.TryParse(ddlMarca.SelectedValue, out idMarca);

            int idCategoria = 0;
            int.TryParse(ddlCategoria.SelectedValue, out idCategoria);

            Producto p = new Producto
            {
                Descripcion = txtDescripcion.Text.Trim(),
                CodigoSKU = txtCodigo.Text.Trim(),
                StockMinimo = stockMin,
                StockActual = stockAct,
                PorcentajeGanancia = ganancia,
                UrlImagen = txtUrlImagen.Text.Trim(),
                Activo = chkActivo.Checked,
                Marca = new Marca { Id = idMarca },
                Categoria = new Categoria { Id = idCategoria }
            };

            // Asigna Id si está en modo edición
            if (ViewState["idProducto"] != null)
                p.Id = (int)ViewState["idProducto"];

            negocio.Guardar(p);

            Response.Redirect("Productos.aspx", false);
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx", false);
        }
    }
}
