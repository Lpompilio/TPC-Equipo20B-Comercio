using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class CompraNegocio
    {
        public void Registrar(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Insertar la compra
                datos.setearConsulta(
                    "INSERT INTO Compras (IdProveedor, Fecha, IdUsuario, Observaciones) " +
                    "OUTPUT INSERTED.Id VALUES (@prov, @fecha, @usuario, @obs)");
                datos.setearParametro("@prov", compra.Proveedor.Id);
                datos.setearParametro("@fecha", compra.Fecha);
                datos.setearParametro("@usuario", compra.Usuario.Id);
                datos.setearParametro("@obs", (object)(compra.Observaciones ?? string.Empty));

                int idCompra = Convert.ToInt32(datos.ejecutarScalar());

                // Insertar las líneas de la compra
                foreach (var linea in compra.Lineas)
                {
                    datos.setearConsulta(@"
                        INSERT INTO compra_lineas (IdCompra, IdProducto, Cantidad, PrecioUnitario)
                        VALUES (@idCompra, @idProd, @cant, @precio)");

                    datos.setearParametro("@idCompra", idCompra);
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.setearParametro("@cant", linea.Cantidad);
                    datos.setearParametro("@precio", linea.PrecioUnitario);
                    datos.ejecutarAccion();

                    // Actualizar stock
                    datos.setearConsulta("UPDATE Productos SET StockActual = StockActual + @cant WHERE Id = @idProd");
                    datos.setearParametro("@cant", linea.Cantidad);
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.ejecutarAccion();

                    // Registrar precio de compra
                    datos.setearConsulta(@"
                         INSERT INTO precios_compra (IdProducto, IdProveedor, Precio, Fecha)
                         VALUES (@idProd, @idProv, @precio, GETDATE())");
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.setearParametro("@idProv", compra.Proveedor.Id);
                    datos.setearParametro("@precio", linea.PrecioUnitario);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // 🟢 Listar con búsqueda opcional
        public List<Compra> Listar(string q = null)
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (string.IsNullOrWhiteSpace(q))
                {
                    datos.setearConsulta(@"
                        SELECT 
                            C.Id,
                            C.Fecha,
                            C.IdProveedor,
                            P.Nombre AS NombreProveedor,
                            C.IdUsuario,
                            U.Nombre AS NombreUsuario,
                            C.Observaciones
                        FROM COMPRAS AS C
                        INNER JOIN PROVEEDORES AS P ON C.IdProveedor = P.Id
                        INNER JOIN USUARIOS AS U ON C.IdUsuario = U.Id
                        ORDER BY C.Fecha DESC;");
                }
                else
                {
                    datos.setearConsulta(@"
                        SELECT 
                            C.Id,
                            C.Fecha,
                            C.IdProveedor,
                            P.Nombre AS NombreProveedor,
                            C.IdUsuario,
                            U.Nombre AS NombreUsuario,
                            C.Observaciones
                        FROM COMPRAS AS C
                        INNER JOIN PROVEEDORES AS P ON C.IdProveedor = P.Id
                        INNER JOIN USUARIOS AS U ON C.IdUsuario = U.Id
                        WHERE 
                            P.Nombre LIKE @q
                            OR U.Nombre LIKE @q
                            OR C.Observaciones LIKE @q
                        ORDER BY C.Fecha DESC;");
                    datos.setearParametro("@q", "%" + q + "%");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Observaciones = datos.Lector["Observaciones"] != DBNull.Value
                            ? (string)datos.Lector["Observaciones"]
                            : string.Empty,
                        Proveedor = new Proveedor
                        {
                            Id = (int)datos.Lector["IdProveedor"],
                            Nombre = (string)datos.Lector["NombreProveedor"]
                        },
                        Usuario = new Usuario
                        {
                            Id = (int)datos.Lector["IdUsuario"],
                            Nombre = (string)datos.Lector["NombreUsuario"]
                        }
                    };

                    lista.Add(compra);
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM COMPRAS WHERE Id = @id");
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
