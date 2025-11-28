using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Equipo20B
{
    public partial class AgregarProducto : Page
    {
        private int idProducto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();

                bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];

                if (esAdmin)
                {
                    txtStockActual.ReadOnly = false;
                    txtStockActual.Enabled = true;
                }
                else
                {
                    txtStockActual.ReadOnly = true;
                    txtStockActual.Enabled = false;
                }

                if (Request.QueryString["id"] != null)
                {
                    idProducto = int.Parse(Request.QueryString["id"]);
                    CargarProducto(idProducto);
                    CargarProveedores(idProducto);
                    lblTitulo.InnerText = "Editar Producto";
                    btnGuardar.Text = "Guardar Cambios";
                }
                else
                {
                    txtStockActual.Text = "0";
                    CargarProveedores(null);
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
            ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca...", "0"));

            ddlCategoria.DataSource = categoriaNegocio.Listar();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría...", "0"));
        }

        private void CargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.ObtenerPorId(id);

            if (producto != null)
            {
                txtDescripcion.Text = producto.Descripcion;
                txtStockMinimo.Text = producto.StockMinimo.ToString();
                txtStockActual.Text = producto.StockActual.ToString();
                txtGanancia.Text = producto.PorcentajeGanancia.ToString();
                chkHabilitado.Checked = producto.Habilitado;

                txtSKU.Text = producto.CodigoSKU;

                if (producto.Marca != null)
                    ddlMarca.SelectedValue = producto.Marca.Id.ToString();

                if (producto.Categoria != null)
                    ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();

                ViewState["idProducto"] = id;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ProductoNegocio negocio = new ProductoNegocio();
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];

            decimal stockMin = 0;
            decimal ganancia = 0;
            decimal stockActual = 0;

            // Usamos la cultura actual para interpretar la coma como separador decimal
            var culture = CultureInfo.CurrentCulture;

            decimal.TryParse(txtStockMinimo.Text, NumberStyles.Any, culture, out stockMin);
            decimal.TryParse(txtGanancia.Text, NumberStyles.Any, culture, out ganancia);

            int idMarca = 0;
            int.TryParse(ddlMarca.SelectedValue, out idMarca);

            int idCategoria = 0;
            int.TryParse(ddlCategoria.SelectedValue, out idCategoria);

            if (idCategoria == 0)
                return;

            // --- Determinar el StockActual según si es alta/edición y si es Admin ---
            if (ViewState["idProducto"] != null)
            {
                int idProd = (int)ViewState["idProducto"];

                // Traemos el producto actual de la BD para no pisar el stock si no es admin
                Producto actual = negocio.ObtenerPorId(idProd);
                decimal stockBD = actual != null ? actual.StockActual : 0;

                if (esAdmin)
                {
                    decimal.TryParse(txtStockActual.Text, NumberStyles.Any, culture, out stockActual);
                }
                else
                {
                    stockActual = stockBD; // el vendedor NO cambia el stock
                }
            }
            else
            {
                if (esAdmin)
                {
                    decimal.TryParse(txtStockActual.Text, NumberStyles.Any, culture, out stockActual);
                }
                else
                {
                    stockActual = 0; // alta de producto por vendedor → arranca en 0
                }
            }

            Producto p = new Producto
            {
                Descripcion = txtDescripcion.Text.Trim(),
                CodigoSKU = txtSKU.Text.Trim(),
                StockMinimo = stockMin,
                StockActual = stockActual,
                PorcentajeGanancia = ganancia,
                Habilitado = chkHabilitado.Checked,
                Marca = new Marca { Id = idMarca },
                Categoria = new Categoria { Id = idCategoria }
            };

            if (ViewState["idProducto"] != null)
                p.Id = (int)ViewState["idProducto"];

            // Guardar producto
            negocio.Guardar(p);

            // Guardar los proveedores asociados
            List<int> proveedoresSeleccionados = new List<int>();

            foreach (GridViewRow row in gvProveedores.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSel");

                if (chk.Checked)
                {
                    int idProv = Convert.ToInt32(gvProveedores.DataKeys[row.RowIndex].Value);
                    proveedoresSeleccionados.Add(idProv);
                }
            }

            ProductoNegocio prodNeg = new ProductoNegocio();
            prodNeg.ActualizarProveedoresProducto(p.Id, proveedoresSeleccionados);

            Response.Redirect("Productos.aspx", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx", false);
        }

        private void CargarProveedores(int? idProd = null)
        {
            ProveedorNegocio provNeg = new ProveedorNegocio();
            gvProveedores.DataSource = provNeg.Listar();
            gvProveedores.DataBind();

            if (idProd.HasValue)
                MarcarProveedoresAsociados(idProd.Value);
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            string q = txtBuscarProveedor.Text.Trim();

            ProveedorNegocio provNeg = new ProveedorNegocio();
            var lista = string.IsNullOrWhiteSpace(q)
                        ? provNeg.Listar()
                        : provNeg.Listar(q);

            gvProveedores.DataSource = lista;
            gvProveedores.DataBind();

            if (ViewState["idProducto"] != null)
            {
                int idProd = (int)ViewState["idProducto"];
                MarcarProveedoresAsociados(idProd);
            }
        }

        private void MarcarProveedoresAsociados(int idProducto)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            var asociados = negocio.ObtenerProveedoresPorProducto(idProducto);

            foreach (GridViewRow row in gvProveedores.Rows)
            {
                int idProv = Convert.ToInt32(gvProveedores.DataKeys[row.RowIndex].Value);
                CheckBox chk = (CheckBox)row.FindControl("chkSel");
                chk.Checked = asociados.Contains(idProv);
            }
        }
    }
}
