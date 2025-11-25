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

                int idCompra = Convert.ToInt32(datos.EjecutarScalar());

                datos.LimpiarParametros();


                // Insertar las líneas
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

                    datos.LimpiarParametros();


                    // Actualizar stock
                    datos.setearConsulta(
                        "UPDATE Productos SET StockActual = StockActual + @cant WHERE Id = @idProd");

                    datos.setearParametro("@cant", linea.Cantidad);
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.ejecutarAccion();

                    datos.LimpiarParametros();


                    // Registrar precio de compra
                    datos.setearConsulta(@"
                INSERT INTO precios_compra (IdProducto, IdProveedor, Precio, Fecha)
                VALUES (@idProd, @idProv, @precio, GETDATE())");

                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.setearParametro("@idProv", compra.Proveedor.Id);
                    datos.setearParametro("@precio", linea.PrecioUnitario);
                    datos.ejecutarAccion();

                    datos.LimpiarParametros();   
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
                string consulta = @"
            SELECT 
                C.Id,
                C.Fecha,
                C.IdProveedor,
                P.Nombre AS NombreProveedor,
                C.IdUsuario,
                U.Nombre AS NombreUsuario,
                C.Observaciones,

                C.Cancelada,
                C.MotivoCancelacion,
                C.FechaCancelacion,
                C.IdUsuarioCancelacion,
                U2.Nombre AS NombreUsuarioCancelacion

            FROM COMPRAS C
            INNER JOIN PROVEEDORES P ON C.IdProveedor = P.Id
            INNER JOIN USUARIOS U ON C.IdUsuario = U.Id
            LEFT JOIN USUARIOS U2 ON C.IdUsuarioCancelacion = U2.Id
        ";

                if (!string.IsNullOrWhiteSpace(q))
                {
                    consulta += @"
                WHERE 
                    P.Nombre LIKE @q OR
                    U.Nombre LIKE @q OR
                    C.Observaciones LIKE @q OR
                    C.MotivoCancelacion LIKE @q
            ";
                }

                consulta += " ORDER BY C.Fecha DESC;";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(q))
                    datos.setearParametro("@q", "%" + q + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],

                        Observaciones = datos.Lector["Observaciones"] as string ?? "",

                        Proveedor = new Proveedor
                        {
                            Id = (int)datos.Lector["IdProveedor"],
                            Nombre = datos.Lector["NombreProveedor"].ToString()
                        },

                        Usuario = new Usuario
                        {
                            Id = (int)datos.Lector["IdUsuario"],
                            Nombre = datos.Lector["NombreUsuario"].ToString()
                        },

                        Cancelada = datos.Lector["Cancelada"] != DBNull.Value
                                    && (bool)datos.Lector["Cancelada"],

                        MotivoCancelacion = datos.Lector["MotivoCancelacion"] as string ?? "",

                        FechaCancelacion = datos.Lector["FechaCancelacion"] is DBNull
                                            ? null
                                            : (DateTime?)datos.Lector["FechaCancelacion"],

                        UsuarioCancelacion =
                            datos.Lector["IdUsuarioCancelacion"] == DBNull.Value
                            ? null
                            : new Usuario
                            {
                                Id = (int)datos.Lector["IdUsuarioCancelacion"],
                                Nombre = datos.Lector["NombreUsuarioCancelacion"].ToString()
                            }
                    };

                    lista.Add(compra);
                }

                return lista;
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

        public void Cancelar(int idCompra, string motivo, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traigo las líneas de la compra
                datos.setearConsulta("SELECT IdProducto, Cantidad FROM COMPRA_LINEAS WHERE IdCompra = @id");
                datos.setearParametro("@id", idCompra);
                datos.ejecutarLectura();

                List<(int idProd, int cant)> lineas = new List<(int, int)>();

                while (datos.Lector.Read())
                {
                    lineas.Add(((int)datos.Lector["IdProducto"], (int)datos.Lector["Cantidad"]));
                }

                datos.CerrarConexion();

                // Ajustar stock sin dejar valores negativos
                foreach (var item in lineas)
                {
                    // Traigo stock actual
                    AccesoDatos get = new AccesoDatos();
                    get.setearConsulta("SELECT StockActual FROM PRODUCTOS WHERE Id = @idProd");
                    get.setearParametro("@idProd", item.idProd);
                    get.ejecutarLectura();

                    int stockActual = 0;
                    if (get.Lector.Read())
                        stockActual = Convert.ToInt32(get.Lector["StockActual"]);

                    get.CerrarConexion();

                    // Calculo stock resultante
                    int nuevoStock = stockActual - item.cant;
                    if (nuevoStock < 0)
                        nuevoStock = 0;      // evitar negativos

                    // Guardo el nuevo stock
                    AccesoDatos upd = new AccesoDatos();
                    upd.setearConsulta("UPDATE PRODUCTOS SET StockActual = @stk WHERE Id = @idProd");
                    upd.setearParametro("@stk", nuevoStock);
                    upd.setearParametro("@idProd", item.idProd);
                    upd.ejecutarAccion();
                    upd.CerrarConexion();
                }

                // Marcar la compra como cancelada
                AccesoDatos updCompra = new AccesoDatos();
                updCompra.setearConsulta(@"
            UPDATE Compras 
            SET Cancelada = 1,
                MotivoCancelacion = @motivo,
                FechaCancelacion = GETDATE(),
                IdUsuarioCancelacion = @idUsuario
            WHERE Id = @idCompra
        ");

                updCompra.setearParametro("@motivo", motivo);
                updCompra.setearParametro("@idUsuario", idUsuario);
                updCompra.setearParametro("@idCompra", idCompra);
                updCompra.ejecutarAccion();
                updCompra.CerrarConexion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



    }
}
