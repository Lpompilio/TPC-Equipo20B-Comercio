using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
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
    V.IdUsuario,
    U.Nombre AS NombreUsuario,
    C.Id AS IdCliente,  
    C.Nombre AS NombreCliente
FROM Ventas V
INNER JOIN Clientes C ON V.IdCliente = C.Id
INNER JOIN Usuarios U ON V.IdUsuario = U.Id
WHERE 1 = 1
";

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
                V.NumeroNC,
                V.MetodoPago,
                V.Total AS TotalBD,
                V.Cancelada,
                V.MotivoCancelacion,
                V.FechaCancelacion,
                V.IdUsuarioCancelacion,
                C.Id AS IdCliente,
                C.Nombre AS NombreCliente,
                C.Email AS EmailCliente,
                U.Nombre AS NombreUsuarioCancelacion,
                V.IdUsuario AS IdVendedor,
                U2.Nombre AS NombreVendedor
            FROM VENTAS V
            INNER JOIN CLIENTES C ON V.IdCliente = C.Id
            LEFT JOIN USUARIOS U ON V.IdUsuarioCancelacion = U.Id
            LEFT JOIN USUARIOS U2 ON V.IdUsuario = U2.Id 
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
                        NumeroNC = datos.Lector["NumeroNC"]?.ToString(),
                        MetodoPago = datos.Lector["MetodoPago"]?.ToString(),
                        TotalBD = datos.Lector["TotalBD"] != DBNull.Value
                            ? Convert.ToDecimal(datos.Lector["TotalBD"])
                            : 0,
                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString(),
                            Email = datos.Lector["EmailCliente"] == DBNull.Value
                                ? null
                                : datos.Lector["EmailCliente"].ToString()
                        },
                        Usuario = datos.Lector["IdVendedor"] == DBNull.Value
                            ? null
                            : new Usuario
                            {
                                Id = Convert.ToInt32(datos.Lector["IdVendedor"]),
                                Nombre = datos.Lector["NombreVendedor"]?.ToString()
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
            finally
            {
                datos.CerrarConexion();
            }
        }

        private string GenerarNumeroFactura()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT TOP 1 NumeroFactura
            FROM VENTAS
            WHERE NumeroFactura LIKE 'R-%'
                  AND NumeroFactura IS NOT NULL
                  AND NumeroFactura <> ''
            ORDER BY Id DESC;
        ");

                object resultado = datos.EjecutarScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return "R-0001-00000001";

                string ultimo = resultado.ToString().Trim();

                int posGuion = ultimo.LastIndexOf('-');
                int correlativoActual;

                if (posGuion >= 0 &&
                    int.TryParse(ultimo.Substring(posGuion + 1), out correlativoActual))
                {
                    correlativoActual++;
                    return $"R-0001-{correlativoActual:00000000}";
                }

                return "R-0001-00000001";
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private string GenerarNumeroNC()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT TOP 1 NumeroNC
                    FROM VENTAS
                    WHERE NumeroNC IS NOT NULL AND NumeroNC <> ''
                    ORDER BY Id DESC;
                ");

                object resultado = datos.EjecutarScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return "NC-0001-00000001";

                string ultimo = resultado.ToString().Trim();

                int posGuion = ultimo.LastIndexOf('-');
                int correlativoActual;

                if (posGuion >= 0 &&
                    int.TryParse(ultimo.Substring(posGuion + 1), out correlativoActual))
                {
                    correlativoActual++;
                    string prefijo = ultimo.Substring(0, posGuion + 1);
                    return $"{prefijo}{correlativoActual:00000000}";
                }

                return "NC-0001-00000001";
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Registrar(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            int idVenta = 0;

            try
            {
                decimal total = 0;
                foreach (var linea in venta.Lineas)
                    total += linea.Subtotal;

                string numeroFactura = GenerarNumeroFactura();
                venta.NumeroFactura = numeroFactura;

                datos.setearConsulta(@"
                    INSERT INTO Ventas (IdUsuario, IdCliente, Fecha, NumeroFactura, MetodoPago, Total)
                    OUTPUT INSERTED.Id
                    VALUES (@usuario, @cliente, @fecha, @factura, @metodo, @total)");
                datos.setearParametro("@usuario", venta.Usuario.Id);
                datos.setearParametro("@cliente", venta.Cliente.Id);
                datos.setearParametro("@fecha", venta.Fecha);
                datos.setearParametro("@factura", numeroFactura);
                datos.setearParametro("@metodo", venta.MetodoPago ?? "");
                datos.setearParametro("@total", total);

                idVenta = Convert.ToInt32(datos.EjecutarScalar());

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

            // ---------- ENVÍO AUTOMÁTICO DE FACTURA (SILENCIOSO) ----------
            try
            {
                Venta ventaCompleta = ObtenerPorId(idVenta);

                if (ventaCompleta != null &&
                    ventaCompleta.Cliente != null &&
                    !string.IsNullOrEmpty(ventaCompleta.Cliente.Email))
                {
                    EnviarFacturaPorMail(ventaCompleta);
                }
            }
            catch
            {
                // silencioso: NO rompe el registro de venta
            }
        }

        public void Cancelar(int idVenta, string motivo, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
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

                string numeroNC = GenerarNumeroNC();

                AccesoDatos updVenta = new AccesoDatos();
                updVenta.setearConsulta(@"
            UPDATE VENTAS
            SET Cancelada = 1,
                MotivoCancelacion = @motivo,
                FechaCancelacion = GETDATE(),
                IdUsuarioCancelacion = @idUsuario,
                NumeroNC = @numeroNC
            WHERE Id = @idVenta
        ");

                updVenta.setearParametro("@motivo", motivo);
                updVenta.setearParametro("@idUsuario", idUsuario);
                updVenta.setearParametro("@numeroNC", numeroNC);
                updVenta.setearParametro("@idVenta", idVenta);
                updVenta.ejecutarAccion();
                updVenta.CerrarConexion();
            }
            finally
            {
                datos.CerrarConexion();
            }

            // ---------- ENVÍO AUTOMÁTICO DE NOTA DE CRÉDITO (SILENCIOSO) ----------
            try
            {
                Venta ventaCancelada = ObtenerPorId(idVenta);

                if (ventaCancelada != null &&
                    ventaCancelada.Cliente != null &&
                    !string.IsNullOrEmpty(ventaCancelada.Cliente.Email))
                {
                    EnviarFacturaPorMail(ventaCancelada); // reutiliza el mismo método
                }
            }
            catch
            {
                // silencioso: no rompe la cancelación
            }
        }

        public decimal ObtenerTotalVentasMes(int? idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT ISNULL(SUM(V.Total), 0)
                    FROM VENTAS V
                    WHERE V.Cancelada = 0
                      AND V.Fecha >= @inicio
                      AND V.Fecha < @fin";

                if (idUsuario.HasValue)
                    consulta += " AND V.IdUsuario = @idUsuario";

                DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fin = inicio.AddMonths(1);

                datos.setearConsulta(consulta);
                datos.setearParametro("@inicio", inicio);
                datos.setearParametro("@fin", fin);

                if (idUsuario.HasValue)
                    datos.setearParametro("@idUsuario", idUsuario.Value);

                object r = datos.EjecutarScalar();
                if (r == null || r == DBNull.Value)
                    return 0;

                return Convert.ToDecimal(r);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int ObtenerPedidosCompletadosMes(int? idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT COUNT(*)
                    FROM VENTAS V
                    WHERE V.Cancelada = 0
                      AND V.Fecha >= @inicio
                      AND V.Fecha < @fin";

                if (idUsuario.HasValue)
                    consulta += " AND V.IdUsuario = @idUsuario";

                DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fin = inicio.AddMonths(1);

                datos.setearConsulta(consulta);
                datos.setearParametro("@inicio", inicio);
                datos.setearParametro("@fin", fin);

                if (idUsuario.HasValue)
                    datos.setearParametro("@idUsuario", idUsuario.Value);

                object r = datos.EjecutarScalar();
                if (r == null || r == DBNull.Value)
                    return 0;

                return Convert.ToInt32(r);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int ObtenerClientesNuevosMes(int? idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT COUNT(*)
            FROM CLIENTES C
            WHERE EXISTS (
                SELECT 1 
                FROM VENTAS V
                WHERE V.IdCliente = C.Id
                  AND V.Cancelada = 0
                  AND V.Fecha >= @inicio
                  AND V.Fecha < @fin";

                if (idUsuario.HasValue)
                    consulta += " AND V.IdUsuario = @idUsuario";

                consulta += @"
            )
            AND NOT EXISTS (
                SELECT 1
                FROM VENTAS V2
                WHERE V2.IdCliente = C.Id
                  AND V2.Cancelada = 0
                  AND V2.Fecha < @inicio";

                if (idUsuario.HasValue)
                    consulta += " AND V2.IdUsuario = @idUsuario";

                consulta += @"
            );";

                DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fin = inicio.AddMonths(1);

                datos.setearConsulta(consulta);
                datos.setearParametro("@inicio", inicio);
                datos.setearParametro("@fin", fin);

                if (idUsuario.HasValue)
                    datos.setearParametro("@idUsuario", idUsuario.Value);

                object r = datos.EjecutarScalar();
                if (r == null || r == DBNull.Value)
                    return 0;

                return Convert.ToInt32(r);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public decimal ObtenerTicketPromedioMes(int? idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT ISNULL(
                        SUM(V.Total) / NULLIF(COUNT(*), 0),
                        0
                    )
                    FROM VENTAS V
                    WHERE V.Cancelada = 0
                      AND V.Fecha >= @inicio
                      AND V.Fecha < @fin";

                if (idUsuario.HasValue)
                    consulta += " AND V.IdUsuario = @idUsuario";

                DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fin = inicio.AddMonths(1);

                datos.setearConsulta(consulta);
                datos.setearParametro("@inicio", inicio);
                datos.setearParametro("@fin", fin);

                if (idUsuario.HasValue)
                    datos.setearParametro("@idUsuario", idUsuario.Value);

                object r = datos.EjecutarScalar();
                if (r == null || r == DBNull.Value)
                    return 0;

                return Convert.ToDecimal(r);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<TopProductoVendido> TopProductosVendidosMes(int? idUsuario)
        {
            List<TopProductoVendido> lista = new List<TopProductoVendido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
                    SELECT TOP 10
                        P.Descripcion AS Producto,
                        C.Nombre AS Categoria,
                        SUM(DV.Cantidad) AS Unidades,
                        SUM(DV.Cantidad * DV.PrecioUnitario) AS Ingresos,
                        U.Nombre AS Vendedor
                    FROM DETALLE_VENTA DV
                    INNER JOIN VENTAS V ON DV.IdVenta = V.Id
                    INNER JOIN PRODUCTOS P ON DV.IdProducto = P.Id
                    INNER JOIN CATEGORIAS C ON P.IdCategoria = C.Id
                    INNER JOIN USUARIOS U ON V.IdUsuario = U.Id
                    WHERE V.Cancelada = 0
                      AND V.Fecha >= @inicio
                      AND V.Fecha < @fin";

                if (idUsuario.HasValue)
                    consulta += " AND V.IdUsuario = @idUsuario";

                consulta += @"
                    GROUP BY P.Descripcion, C.Nombre, U.Nombre
                    ORDER BY Unidades DESC";

                DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fin = inicio.AddMonths(1);

                datos.setearConsulta(consulta);
                datos.setearParametro("@inicio", inicio);
                datos.setearParametro("@fin", fin);

                if (idUsuario.HasValue)
                    datos.setearParametro("@idUsuario", idUsuario.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var item = new TopProductoVendido
                    {
                        Producto = datos.Lector["Producto"].ToString(),
                        Categoria = datos.Lector["Categoria"].ToString(),
                        Unidades = Convert.ToDecimal(datos.Lector["Unidades"]),
                        Ingresos = Convert.ToDecimal(datos.Lector["Ingresos"]),
                        Vendedor = datos.Lector["Vendedor"].ToString()
                    };

                    lista.Add(item);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // ----------------------------------------------------
        // ENVÍO DE MAIL
        // ----------------------------------------------------
        public void EnviarFacturaPorMail(Venta venta)
        {
            EmailService.EnviarFactura(venta);
        }

        public void EnviarMailFactura(Venta venta)
        {
            EnviarFacturaPorMail(venta);
        }
    }
}
