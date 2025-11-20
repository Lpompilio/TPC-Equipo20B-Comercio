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

                // detectar edición
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
                //txtCodigo.Text = producto.CodigoSKU;
                txtStockMinimo.Text = producto.StockMinimo.ToString();
                txtStockActual.Text = producto.StockActual.ToString();
                txtGanancia.Text = producto.PorcentajeGanancia.ToString();
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

            if (idCategoria == 0)
            {
                
                return;
            }

            Producto p = new Producto
            {
                Descripcion = txtDescripcion.Text.Trim(),
                StockMinimo = stockMin,
                StockActual = stockAct,
                PorcentajeGanancia = ganancia,
                Activo = chkActivo.Checked,
                Marca = new Marca { Id = idMarca },
                Categoria = new Categoria { Id = idCategoria }
            };

            if (idMarca > 0)
                p.Marca = new Marca { Id = idMarca };

            // Asigna Id si está en modo edición
            if (ViewState["idProducto"] != null)
                p.Id = (int)ViewState["idProducto"];

            negocio.Guardar(p);

            // --- Guardar proveedores asociados ---
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

            // volver a marcar seleccionados si es edición
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
