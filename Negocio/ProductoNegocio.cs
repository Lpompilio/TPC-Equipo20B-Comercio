using System;
using System.Collections.Generic;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
            SELECT 
                P.Id, P.CodigoSKU, P.Descripcion, 
                P.StockMinimo, P.StockActual, P.PorcentajeGanancia, 
                P.UrlImagen, P.Activo,
                C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
                M.Id AS IdMarca, M.Nombre AS NombreMarca
            FROM 
                PRODUCTOS AS P
            INNER JOIN 
                CATEGORIAS AS C ON P.IdCategoria = C.Id
            LEFT JOIN 
                MARCAS AS M ON P.IdMarca = M.Id
            WHERE 
                P.Activo = 1";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto p = new Producto();
                    p.Categoria = new Categoria();
                    p.Marca = new Marca();

                    p.Id = (int)datos.Lector["Id"];
                    p.CodigoSKU = datos.Lector["CodigoSKU"] is DBNull ? "" : (string)datos.Lector["CodigoSKU"];
                    p.Descripcion = (string)datos.Lector["Descripcion"];
                    p.StockMinimo = datos.Lector["StockMinimo"] is DBNull ? 0 : (decimal)datos.Lector["StockMinimo"];
                    p.StockActual = datos.Lector["StockActual"] is DBNull ? 0 : (decimal)datos.Lector["StockActual"];
                    p.PorcentajeGanancia = datos.Lector["PorcentajeGanancia"] is DBNull ? 0 : (decimal)datos.Lector["PorcentajeGanancia"];
                    p.UrlImagen = datos.Lector["UrlImagen"] is DBNull ? "" : (string)datos.Lector["UrlImagen"];
                    p.Activo = (bool)datos.Lector["Activo"];

                    p.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    p.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        p.Marca.Id = (int)datos.Lector["IdMarca"];
                        p.Marca.Nombre = (string)datos.Lector["NombreMarca"];
                    }
                    else
                    {
                        p.Marca = null;
                    }
                    lista.Add(p);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Producto ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Usamos JOINs para traer los datos de las tablas relacionadas
                string consulta = @"
            SELECT 
                P.Id, P.CodigoSKU, P.Descripcion, 
                P.StockMinimo, P.StockActual, P.PorcentajeGanancia, 
                P.UrlImagen, P.Activo,
                C.Id AS IdCategoria, C.Nombre AS NombreCategoria,
                M.Id AS IdMarca, M.Nombre AS NombreMarca,
                PR.Id AS IdProveedor, PR.Nombre AS NombreProveedor
            FROM 
                PRODUCTOS AS P
            INNER JOIN 
                CATEGORIAS AS C ON P.IdCategoria = C.Id
            LEFT JOIN 
                MARCAS AS M ON P.IdMarca = M.Id
            LEFT JOIN 
                PROVEEDORES AS PR ON P.IdProveedor = PR.Id
            WHERE 
                P.Id = @id";

                datos.setearConsulta(consulta);
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Producto p = new Producto();

                    // Instanciamos los objetos anidados para rellenarlos
                    p.Categoria = new Categoria();
                    p.Marca = new Marca();
                    p.Proveedor = new Proveedor();

                    // Carga de datos simples
                    p.Id = (int)datos.Lector["Id"];
                    p.CodigoSKU = datos.Lector["CodigoSKU"] is DBNull ? "" : (string)datos.Lector["CodigoSKU"];
                    p.Descripcion = (string)datos.Lector["Descripcion"];
                    p.StockMinimo = datos.Lector["StockMinimo"] is DBNull ? 0 : (decimal)datos.Lector["StockMinimo"];
                    p.StockActual = datos.Lector["StockActual"] is DBNull ? 0 : (decimal)datos.Lector["StockActual"];
                    p.PorcentajeGanancia = datos.Lector["PorcentajeGanancia"] is DBNull ? 0 : (decimal)datos.Lector["PorcentajeGanancia"];
                    p.UrlImagen = datos.Lector["UrlImagen"] is DBNull ? "" : (string)datos.Lector["UrlImagen"];
                    p.Activo = (bool)datos.Lector["Activo"];

                    // Carga de Categoria (Nunca es NULL porque es INNER JOIN)
                    p.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    p.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    // Carga de Marca (Puede ser NULL porque es LEFT JOIN)
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        p.Marca.Id = (int)datos.Lector["IdMarca"];
                        p.Marca.Nombre = (string)datos.Lector["NombreMarca"];
                    }
                    else
                    {
                        p.Marca = null; // Si no hay marca, el objeto queda null
                    }

                    // Carga de Proveedor (Puede ser NULL porque es LEFT JOIN)
                    if (!(datos.Lector["IdProveedor"] is DBNull))
                    {
                        p.Proveedor.Id = (int)datos.Lector["IdProveedor"];
                        p.Proveedor.Nombre = (string)datos.Lector["NombreProveedor"];
                    }
                    else
                    {
                        p.Proveedor = null; // Si no hay proveedor, el objeto queda null
                    }

                    return p;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Guardar(Producto p)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (p.Id == 0)
                {
                    
                    datos.setearConsulta(
                        "INSERT INTO Productos (CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, UrlImagen, Activo, IdCategoria, IdMarca, IdProveedor) " +
                        "VALUES (@sku, @desc, @min, @act, @gan, @img, @actv, @idCat, @idMar, @idProv); " +
                        "SELECT SCOPE_IDENTITY();"
                    );
                    
                    datos.setearParametro("@sku", "SKU_TEMP");
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@act", p.StockActual);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@img", p.UrlImagen);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);

                    if (p.Marca != null && p.Marca.Id > 0)
                        datos.setearParametro("@idMar", p.Marca.Id);
                    else
                        datos.setearParametro("@idMar", DBNull.Value);

                    if (p.Proveedor != null && p.Proveedor.Id > 0)
                        datos.setearParametro("@idProv", p.Proveedor.Id);
                    else
                        datos.setearParametro("@idProv", DBNull.Value);

                    int nuevoId = Convert.ToInt32(datos.ejecutarScalar());
                    p.Id = nuevoId;

                    
                    string nuevoSku = nuevoId.ToString();

                    
                    datos.setearConsulta("UPDATE Productos SET CodigoSKU = @sku WHERE Id = @id");
                    datos.setearParametro("@sku", nuevoSku);
                    datos.setearParametro("@id", p.Id);

                    datos.ejecutarAccion();
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(p.CodigoSKU))
                    {
                        p.CodigoSKU = "SKU" + p.Id.ToString("D5");
                    }

                    datos.setearConsulta("UPDATE Productos SET CodigoSKU=@sku, Descripcion=@desc, StockMinimo=@min, StockActual=@act, PorcentajeGanancia=@gan, UrlImagen=@img, Activo=@actv, IdCategoria=@idCat, IdMarca=@idMar, IdProveedor=@idProv WHERE Id=@id");
                    datos.setearParametro("@id", p.Id);
                    datos.setearParametro("@sku", p.CodigoSKU);
                    datos.setearParametro("@desc", p.Descripcion);
                    datos.setearParametro("@min", p.StockMinimo);
                    datos.setearParametro("@act", p.StockActual);
                    datos.setearParametro("@gan", p.PorcentajeGanancia);
                    datos.setearParametro("@img", p.UrlImagen);
                    datos.setearParametro("@actv", p.Activo);
                    datos.setearParametro("@idCat", p.Categoria.Id);

                    if (p.Marca != null && p.Marca.Id > 0)
                        datos.setearParametro("@idMar", p.Marca.Id);
                    else
                        datos.setearParametro("@idMar", DBNull.Value);

                    if (p.Proveedor != null && p.Proveedor.Id > 0)
                        datos.setearParametro("@idProv", p.Proveedor.Id);
                    else
                        datos.setearParametro("@idProv", DBNull.Value);

                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}

