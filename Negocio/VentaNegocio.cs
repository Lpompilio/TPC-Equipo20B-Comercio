using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        // Ahora soporta búsqueda opcional
        public List<Venta> Listar(string q = null)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
SELECT 
    V.Id,
    V.Fecha,
    V.NumeroFactura,
    V.MetodoPago,
    V.Total,
    V.Cancelada,
    V.MotivoCancelacion,
    V.FechaCancelacion,
    V.IdUsuarioCancelacion,

    -- Usuario que hizo la venta
    V.IdUsuario,
    U.Nombre AS NombreUsuario,

    -- Cliente
    C.Id AS IdCliente,  
    C.Nombre AS NombreCliente

FROM Ventas V
INNER JOIN Clientes C ON V.IdCliente = C.Id
INNER JOIN Usuarios U ON V.IdUsuario = U.Id
WHERE 1 = 1
";

                // Si hay búsqueda → agregamos filtro
                if (!string.IsNullOrWhiteSpace(q))
                {
                    consulta += @"
 AND (
        C.Nombre LIKE @q
        OR V.NumeroFactura LIKE @q
        OR V.MetodoPago LIKE @q
        OR CONVERT(VARCHAR(10), V.Id) LIKE @q
     )
";
                }

                consulta += " ORDER BY V.Fecha DESC";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(q))
                    datos.setearParametro("@q", "%" + q + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta v = new Venta
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        NumeroFactura = datos.Lector["NumeroFactura"]?.ToString(),
                        MetodoPago = datos.Lector["MetodoPago"]?.ToString(),

                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString()
                        },

                        Usuario = new Usuario
                        {
                            Id = (int)datos.Lector["IdUsuario"],
                            Nombre = datos.Lector["NombreUsuario"]?.ToString()
                        },

                        TotalBD = datos.Lector["Total"] != DBNull.Value
                            ? Convert.ToDecimal(datos.Lector["Total"])
                            : 0,

                        Cancelada = Convert.ToBoolean(datos.Lector["Cancelada"])
                    };

                    lista.Add(v);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



        public Venta ObtenerPorId(int id)
        {
            Venta venta = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                V.Id,
                V.Fecha,
                V.NumeroFactura,
                V.MetodoPago,
                V.Total AS TotalBD,
                V.Cancelada,
                V.MotivoCancelacion,
                V.FechaCancelacion,
                V.IdUsuarioCancelacion,

                C.Id AS IdCliente,
                C.Nombre AS NombreCliente,

                U.Nombre AS NombreUsuarioCancelacion
            FROM VENTAS V
            INNER JOIN CLIENTES C ON V.IdCliente = C.Id
            LEFT JOIN USUARIOS U ON V.IdUsuarioCancelacion = U.Id
            WHERE V.Id = @id
        ");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    venta = new Venta
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        NumeroFactura = datos.Lector["NumeroFactura"]?.ToString(),
                        MetodoPago = datos.Lector["MetodoPago"]?.ToString(),

                        // TOTAL DE BD lo guardamos separado
                        TotalBD = datos.Lector["TotalBD"] != DBNull.Value
                            ? Convert.ToDecimal(datos.Lector["TotalBD"])
                            : 0,

                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString()
                        },

                        Cancelada = datos.Lector["Cancelada"] != DBNull.Value && (bool)datos.Lector["Cancelada"],
                        MotivoCancelacion = datos.Lector["MotivoCancelacion"]?.ToString(),
                        FechaCancelacion = datos.Lector["FechaCancelacion"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(datos.Lector["FechaCancelacion"]),

                        UsuarioCancelacion = datos.Lector["IdUsuarioCancelacion"] == DBNull.Value
                            ? null
                            : new Usuario
                            {
                                Id = Convert.ToInt32(datos.Lector["IdUsuarioCancelacion"]),
                                Nombre = datos.Lector["NombreUsuarioCancelacion"]?.ToString()
                            },

                        Lineas = new List<VentaLinea>()
                    };
                }
            }
            finally
            {
                datos.CerrarConexion();
            }

            if (venta != null)
                venta.Lineas = ListarLineasPorVenta(id);

            return venta;
        }



        private List<VentaLinea> ListarLineasPorVenta(int idVenta)
        {
            List<VentaLinea> lineas = new List<VentaLinea>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT VL.Id, VL.Cantidad, VL.PrecioUnitario,
                           P.Id AS IdProducto, P.Descripcion AS NombreProducto
                    FROM DETALLE_VENTA VL
                    INNER JOIN Productos P ON VL.IdProducto = P.Id
                    WHERE VL.IdVenta = @idVenta
                ");
                datos.setearParametro("@idVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    VentaLinea l = new VentaLinea
                    {
                        Id = (int)datos.Lector["Id"],
                        Cantidad = Convert.ToDecimal(datos.Lector["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(datos.Lector["PrecioUnitario"]),
                        Producto = new Producto
                        {
                            Id = (int)datos.Lector["IdProducto"],
                            Descripcion = datos.Lector["NombreProducto"].ToString()
                        }
                    };
                    lineas.Add(l);
                }
                return lineas;
            }
            finally { datos.CerrarConexion(); }
        }

        public void Registrar(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                decimal total = 0;
                foreach (var linea in venta.Lineas)
                    total += linea.Subtotal;

                // Insertar la venta principal
                datos.setearConsulta(@"
                    INSERT INTO Ventas (IdUsuario, IdCliente, Fecha, NumeroFactura, MetodoPago, Total)
                    OUTPUT INSERTED.Id
                    VALUES (@usuario, @cliente, @fecha, @factura, @metodo, @total)");
                datos.setearParametro("@usuario", venta.Usuario.Id);
                datos.setearParametro("@cliente", venta.Cliente.Id);
                datos.setearParametro("@fecha", venta.Fecha);
                datos.setearParametro("@factura", venta.NumeroFactura ?? "");
                datos.setearParametro("@metodo", venta.MetodoPago ?? "");
                datos.setearParametro("@total", total);

                int idVenta = Convert.ToInt32(datos.EjecutarScalar());

                // Insertar las líneas de la venta
                foreach (var linea in venta.Lineas)
                {
                    datos.setearConsulta(@"
                        INSERT INTO Detalle_Venta (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                        VALUES (@idVenta, @idProd, @cant, @precio)");
                    datos.setearParametro("@idVenta", idVenta);
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.setearParametro("@cant", linea.Cantidad);
                    datos.setearParametro("@precio", linea.PrecioUnitario);
                    datos.ejecutarAccion();

                    // Actualizar stock (restar)
                    datos.setearConsulta("UPDATE Productos SET StockActual = StockActual - @cant WHERE Id = @idProd");
                    datos.setearParametro("@cant", linea.Cantidad);
                    datos.setearParametro("@idProd", linea.Producto.Id);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Cancelar(int idVenta, string motivo, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traemos líneas de la venta
                datos.setearConsulta("SELECT IdProducto, Cantidad FROM DETALLE_VENTA WHERE IdVenta = @id");
                datos.setearParametro("@id", idVenta);
                datos.ejecutarLectura();

                List<Tuple<int, decimal>> lineas = new List<Tuple<int, decimal>>();

                while (datos.Lector.Read())
                {
                    lineas.Add(new Tuple<int, decimal>(
                        (int)datos.Lector["IdProducto"],
                        (decimal)datos.Lector["Cantidad"]
                    ));
                }

                datos.CerrarConexion();

                // DEVOLVER stock (lo contrario de la venta)
                foreach (var item in lineas)
                {
                    AccesoDatos upd = new AccesoDatos();
                    upd.setearConsulta("UPDATE PRODUCTOS SET StockActual = StockActual + @cant WHERE Id = @idProd");
                    int idProd = item.Item1;
                    decimal cant = item.Item2;
                    upd.setearParametro("@cant", cant);
                    upd.setearParametro("@idProd", idProd);
                    upd.ejecutarAccion();
                    upd.CerrarConexion();
                }

                // Marcar venta como CANCELADA
                AccesoDatos updVenta = new AccesoDatos();
                updVenta.setearConsulta(@"
            UPDATE VENTAS
            SET Cancelada = 1,
                MotivoCancelacion = @motivo,
                FechaCancelacion = GETDATE(),
                IdUsuarioCancelacion = @idUsuario
            WHERE Id = @idVenta
        ");

                updVenta.setearParametro("@motivo", motivo);
                updVenta.setearParametro("@idUsuario", idUsuario);
                updVenta.setearParametro("@idVenta", idVenta);
                updVenta.ejecutarAccion();
                updVenta.CerrarConexion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
