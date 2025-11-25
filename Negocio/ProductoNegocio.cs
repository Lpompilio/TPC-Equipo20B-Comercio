using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar(string q = null, int? idProveedor = null)
        {
            var lista = new List<Producto>();
            var datos = new AccesoDatos();

            try
            {
                string query = @"
            SELECT DISTINCT
                P.Id, P.CodigoSKU, P.Descripcion, P.StockMinimo, P.StockActual,
                P.PorcentajeGanancia, P.Activo,
                C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
                M.Id AS IdMarca, M.Nombre AS NombreMarca
            FROM PRODUCTOS P
            INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
            LEFT JOIN MARCAS M ON P.IdMarca = M.Id
            LEFT JOIN PRODUCTO_PROVEEDOR PP ON PP.IdProducto = P.Id
            LEFT JOIN PROVEEDORES PR ON PR.Id = PP.IdProveedor
            WHERE P.Activo = 1";

                // FILTRO POR PROVEEDOR
                if (idProveedor.HasValue && idProveedor.Value > 0)
                    query += " AND PP.IdProveedor = @idProv";

                // FILTRO POR BUSQUEDA
                if (!string.IsNullOrWhiteSpace(q))
                    query += @" AND (
                            P.Descripcion LIKE @q 
                            OR P.CodigoSKU LIKE @q 
                            OR C.Nombre LIKE @q 
                            OR M.Nombre LIKE @q
                            OR PR.Nombre LIKE @q
                        )";

                query += " ORDER BY P.Descripcion";

                datos.setearConsulta(query);

                if (idProveedor.HasValue && idProveedor.Value > 0)
                    datos.setearParametro("@idProv", idProveedor.Value);

                if (!string.IsNullOrWhiteSpace(q))
                    datos.setearParametro("@q", "%" + q + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var p = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        CodigoSKU = datos.Lector["CodigoSKU"] as string ?? "",
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        StockMinimo = datos.Lector["StockMinimo"] is DBNull ? 0 : (decimal)datos.Lector["StockMinimo"],
                        StockActual = datos.Lector["StockActual"] is DBNull ? 0 : (decimal)datos.Lector["StockActual"],
                        PorcentajeGanancia = datos.Lector["PorcentajeGanancia"] is DBNull ? 0 : (decimal)datos.Lector["PorcentajeGanancia"],
                        Activo = (bool)datos.Lector["Activo"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = (string)datos.Lector["NombreCategoria"]
                        },
                        Marca = datos.Lector["IdMarca"] is DBNull ? null : new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Nombre = (string)datos.Lector["NombreMarca"]
                        }
                    };

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public Producto ObtenerPorId(int id)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
    SELECT 
        P.Id, P.CodigoSKU, P.Descripcion, P.StockMinimo, P.StockActual, P.PorcentajeGanancia, 
        P.Activo,
        C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
        M.Id AS IdMarca, M.Nombre AS NombreMarca
    FROM PRODUCTOS P
    INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
    LEFT JOIN MARCAS M ON P.IdMarca = M.Id
    WHERE P.Id = @id");


                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Producto p = null;

                if (datos.Lector.Read())
                {
                    p = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        CodigoSKU = datos.Lector["CodigoSKU"] is DBNull ? "" : (string)datos.Lector["CodigoSKU"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        StockMinimo = datos.Lector["StockMinimo"] is DBNull ? 0 : (decimal)datos.Lector["StockMinimo"],
                        StockActual = datos.Lector["StockActual"] is DBNull ? 0 : (decimal)datos.Lector["StockActual"],
                        PorcentajeGanancia = datos.Lector["PorcentajeGanancia"] is DBNull ? 0 : (decimal)datos.Lector["PorcentajeGanancia"],
                        Activo = (bool)datos.Lector["Activo"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = (string)datos.Lector["NombreCategoria"]
                        },
                        Marca = datos.Lector["IdMarca"] is DBNull
                            ? null
                            : new Marca
                            {
                                Id = (int)datos.Lector["IdMarca"],
                                Nombre = (string)datos.Lector["NombreMarca"]
                            },

                    };
                }

                if (p != null)
                {
                    var datosPrecios = new AccesoDatos();
                    datosPrecios.setearConsulta("SELECT Precio, Fecha FROM PRECIOS_COMPRA WHERE IdProducto = @id");
                    datosPrecios.setearParametro("@id", id);
                    datosPrecios.ejecutarLectura();

                    while (datosPrecios.Lector.Read())
                    {
                        var precio = new PrecioCompra
                        {
                            ProductoId = id,
                            PrecioUnitario = (decimal)datosPrecios.Lector["Precio"],
                            Fecha = (DateTime)datosPrecios.Lector["Fecha"]
                        };

                        p.PreciosCompra.Add(precio);
                    }

                    datosPrecios.CerrarConexion();
                }

                return p;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private bool ExisteSku(string sku, int? idProductoExcluir = null)
        {
            var datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT COUNT(*) Cant FROM PRODUCTOS WHERE CodigoSKU = @sku";

                if (idProductoExcluir.HasValue)
                    consulta += " AND Id <> @id";

                datos.setearConsulta(consulta);
                datos.setearParametro("@sku", sku);

                if (idProductoExcluir.HasValue)
                    datos.setearParametro("@id", idProductoExcluir.Value);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector["Cant"]) > 0;

                return false;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Guardar(Producto p)
        {
            var datos = new AccesoDatos();

            try
            {
                int? idExcluir = p.Id == 0 ? (int?)null : p.Id;
                if (ExisteSku(p.CodigoSKU, idExcluir))
                    throw new Exception("Ya existe un producto con el mismo código SKU.");

                if (p.Id == 0)
                {
                    datos.setearConsulta(@"
                        INSERT INTO PRODUCTOS
                            (CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, Activo, IdCategoria, IdMarca)
                        VALUES
                            (@sku, @desc, @min, @act, @gan, @actv, @idCat, @idMar);
                        SELECT SCOPE_IDENTITY();");

                    datos.setearParametro("@sku", p.CodigoSKU);
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@act", p.StockActual);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);
                    datos.setearParametro("@idMar", p.Marca != null && p.Marca.Id > 0 ? (object)p.Marca.Id : DBNull.Value);

                    var nuevoId = Convert.ToInt32(datos.EjecutarScalar());
                    p.Id = nuevoId;
                }
                else
                {
                    datos.setearConsulta(@"
                        UPDATE PRODUCTOS SET
                            CodigoSKU = @sku,
                            Descripcion = @desc,
                            StockMinimo = @min,
                            PorcentajeGanancia = @gan,
                            Activo = @actv,
                            IdCategoria = @idCat,
                            IdMarca = @idMar
                        WHERE Id = @id");

                    datos.setearParametro("@id", p.Id);
                    datos.setearParametro("@sku", p.CodigoSKU);
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);
                    datos.setearParametro("@idMar", p.Marca != null && p.Marca.Id > 0 ? (object)p.Marca.Id : DBNull.Value);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET Activo = 0 WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<int> ObtenerProveedoresPorProducto(int idProducto)
        {
            var lista = new List<int>();
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdProveedor FROM PRODUCTO_PROVEEDOR WHERE IdProducto = @id");
                datos.setearParametro("@id", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    lista.Add((int)datos.Lector["IdProveedor"]);

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ActualizarProveedoresProducto(int idProducto, List<int> proveedores)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM PRODUCTO_PROVEEDOR WHERE IdProducto = @id");
                datos.setearParametro("@id", idProducto);
                datos.ejecutarAccion();

                foreach (int idProv in proveedores)
                {
                    datos.setearConsulta("INSERT INTO PRODUCTO_PROVEEDOR (IdProducto, IdProveedor) VALUES (@p, @prov)");
                    datos.setearParametro("@p", idProducto);
                    datos.setearParametro("@prov", idProv);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Producto> ListarStockBajo()
        {
            var lista = new List<Producto>();
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        P.Id, P.CodigoSKU, P.Descripcion, P.StockMinimo, P.StockActual,
                        C.Id AS IdCategoria, C.Nombre AS NombreCategoria
                    FROM PRODUCTOS P
                    INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
                    WHERE P.Activo = 1
                      AND P.StockMinimo IS NOT NULL
                      AND P.StockActual IS NOT NULL
                      AND P.StockActual < P.StockMinimo
                    ORDER BY P.Descripcion");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var p = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        CodigoSKU = datos.Lector["CodigoSKU"] as string ?? "",
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        StockMinimo = datos.Lector["StockMinimo"] is DBNull ? 0 : (decimal)datos.Lector["StockMinimo"],
                        StockActual = datos.Lector["StockActual"] is DBNull ? 0 : (decimal)datos.Lector["StockActual"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = datos.Lector["NombreCategoria"].ToString()
                        }
                    };

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public List<Producto> listarPorProveedor(int idProveedor)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            datos.setearConsulta(
                "SELECT P.Id, P.Descripcion " +
                "FROM PRODUCTOS P " +
                "INNER JOIN PRODUCTO_PROVEEDOR PP ON PP.IdProducto = P.Id " +
                "WHERE PP.IdProveedor = @id"
            );
            datos.setearParametro("@id", idProveedor);

            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                Producto aux = new Producto();
                aux.Id = (int)datos.Lector["Id"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                lista.Add(aux);
            }
            datos.CerrarConexion();
            return lista;
        }

        public bool ProductoPerteneceAProveedor(int idProducto, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT COUNT(*) FROM PRODUCTO_PROVEEDOR " +
                    "WHERE IdProducto = @prod AND IdProveedor = @prov");
                datos.setearParametro("@prod", idProducto);
                datos.setearParametro("@prov", idProveedor);

                int count = (int)datos.EjecutarScalar();

                return count > 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


    }
}
