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
            SELECT 
                P.Id, P.CodigoSKU, P.Descripcion, P.StockMinimo, P.StockActual,
                P.PorcentajeGanancia, P.UrlImagen, P.Activo,
                C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
                M.Id AS IdMarca, M.Nombre AS NombreMarca,
                PR.Id AS IdProveedor, PR.Nombre AS NombreProveedor
            FROM PRODUCTOS P
            INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
            LEFT JOIN MARCAS M ON P.IdMarca = M.Id
            LEFT JOIN PROVEEDORES PR ON P.IdProveedor = PR.Id
            WHERE P.Activo = 1";

                if (idProveedor.HasValue && idProveedor.Value > 0)
                {
                    query += " AND EXISTS (SELECT 1 FROM PRODUCTO_PROVEEDOR PP WHERE PP.IdProducto = P.Id AND PP.IdProveedor = @idProv)";
                }

                if (!string.IsNullOrWhiteSpace(q))
                {
                    query += " AND (P.Descripcion LIKE @q OR P.CodigoSKU LIKE @q OR C.Nombre LIKE @q OR M.Nombre LIKE @q OR PR.Nombre LIKE @q)";
                }

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
                        UrlImagen = datos.Lector["UrlImagen"] as string ?? "",
                        Activo = (bool)datos.Lector["Activo"],
                        Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = (string)datos.Lector["NombreCategoria"]
                        },
                        Marca = datos.Lector["IdMarca"] is DBNull ? null :
                                new Marca { Id = (int)datos.Lector["IdMarca"], Nombre = (string)datos.Lector["NombreMarca"] },
                        Proveedor = datos.Lector["IdProveedor"] is DBNull ? null :
                                    new Proveedor { Id = (int)datos.Lector["IdProveedor"], Nombre = (string)datos.Lector["NombreProveedor"] }
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
                P.UrlImagen, P.Activo,
                C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
                M.Id AS IdMarca, M.Nombre AS NombreMarca,
                PR.Id AS IdProveedor, PR.Nombre AS NombreProveedor
            FROM PRODUCTOS P
            INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
            LEFT JOIN MARCAS M ON P.IdMarca = M.Id
            LEFT JOIN PROVEEDORES PR ON P.IdProveedor = PR.Id
            WHERE P.Id = @id
        ");
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
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? "" : (string)datos.Lector["UrlImagen"],
                        Activo = (bool)datos.Lector["Activo"],
                        Categoria = new Categoria { Id = (int)datos.Lector["IdCategoria"], Nombre = (string)datos.Lector["NombreCategoria"] },
                        Marca = datos.Lector["IdMarca"] is DBNull ? null : new Marca { Id = (int)datos.Lector["IdMarca"], Nombre = (string)datos.Lector["NombreMarca"] },
                        Proveedor = datos.Lector["IdProveedor"] is DBNull ? null : new Proveedor { Id = (int)datos.Lector["IdProveedor"], Nombre = (string)datos.Lector["NombreProveedor"] }
                    };
                }

                datos.CerrarConexion();

                // cargar los precios de compra
                if (p != null)
                {
                    var datosPrecios = new AccesoDatos();
                    datosPrecios.setearConsulta("SELECT Precio, Fecha FROM PRECIOS_COMPRA WHERE IdProducto = @id");
                    datosPrecios.setearParametro("@id", id);
                    datosPrecios.ejecutarLectura();

                    while (datosPrecios.Lector.Read())
                    {
                        PrecioCompra precio = new PrecioCompra
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


        public void Guardar(Producto p)
        {
            var datos = new AccesoDatos();
            try
            {
                if (p.Id == 0)
                {
                    datos.setearConsulta(
                        "INSERT INTO Productos (CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, UrlImagen, Activo, IdCategoria, IdMarca, IdProveedor) " +
                        "VALUES (@sku, @desc, @min, @act, @gan, @img, @actv, @idCat, @idMar, @idProv); SELECT SCOPE_IDENTITY();"
                    );
                    datos.setearParametro("@sku", "SKU_TEMP");
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@act", p.StockActual);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@img", p.UrlImagen);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);
                    datos.setearParametro("@idMar", p.Marca != null && p.Marca.Id > 0 ? (object)p.Marca.Id : DBNull.Value);
                    datos.setearParametro("@idProv", p.Proveedor != null && p.Proveedor.Id > 0 ? (object)p.Proveedor.Id : DBNull.Value);
                    var nuevoId = Convert.ToInt32(datos.EjecutarScalar());
                    p.Id = nuevoId;
                    var nuevoSku = nuevoId.ToString();
                    datos.setearConsulta("UPDATE Productos SET CodigoSKU=@sku WHERE Id=@id");
                    datos.setearParametro("@sku", nuevoSku);
                    datos.setearParametro("@id", p.Id);
                    datos.ejecutarAccion();
                }
                else
                {
                    if (string.IsNullOrEmpty(p.CodigoSKU))
                        p.CodigoSKU = "SKU" + p.Id.ToString("D5");

                    datos.setearConsulta(@"
                        UPDATE Productos SET
                        CodigoSKU=@sku, Descripcion=@desc, StockMinimo=@min, StockActual=@act, PorcentajeGanancia=@gan, UrlImagen=@img, Activo=@actv,
                        IdCategoria=@idCat, IdMarca=@idMar, IdProveedor=@idProv
                        WHERE Id=@id
                    ");
                    datos.setearParametro("@id", p.Id);
                    datos.setearParametro("@sku", p.CodigoSKU);
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@act", p.StockActual);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@img", p.UrlImagen);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);
                    datos.setearParametro("@idMar", p.Marca != null && p.Marca.Id > 0 ? (object)p.Marca.Id : DBNull.Value);
                    datos.setearParametro("@idProv", p.Proveedor != null && p.Proveedor.Id > 0 ? (object)p.Proveedor.Id : DBNull.Value);
                    datos.ejecutarAccion();
                }
            }
            finally { datos.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

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
            List<int> lista = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdProveedor FROM PRODUCTO_PROVEEDOR WHERE IdProducto = @id");
                datos.setearParametro("@id", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    lista.Add((int)datos.Lector["IdProveedor"]);

                return lista;
            }
            finally { datos.CerrarConexion(); }
        }

        public void ActualizarProveedoresProducto(int idProducto, List<int> proveedores)
        {
            AccesoDatos datos = new AccesoDatos();

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
            finally { datos.CerrarConexion(); }
        }
    }
}
