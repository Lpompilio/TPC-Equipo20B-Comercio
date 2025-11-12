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
                if (string.IsNullOrWhiteSpace(q))
                {
                    datos.setearConsulta(@"
                        SELECT 
                            V.Id,
                            V.Fecha,
                            V.NumeroFactura,
                            V.MetodoPago,
                            V.Total,
                            C.Id AS IdCliente,
                            C.Nombre AS NombreCliente
                        FROM Ventas V
                        INNER JOIN Clientes C ON V.IdCliente = C.Id
                        ORDER BY V.Fecha DESC
                    ");
                }
                else
                {
                    datos.setearConsulta(@"
                        SELECT 
                            V.Id,
                            V.Fecha,
                            V.NumeroFactura,
                            V.MetodoPago,
                            V.Total,
                            C.Id AS IdCliente,
                            C.Nombre AS NombreCliente
                        FROM Ventas V
                        INNER JOIN Clientes C ON V.IdCliente = C.Id
                        WHERE 
                            C.Nombre LIKE @q
                            OR V.NumeroFactura LIKE @q
                            OR V.MetodoPago LIKE @q
                            OR CONVERT(VARCHAR(10), V.Id) LIKE @q
                        ORDER BY V.Fecha DESC
                    ");
                    datos.setearParametro("@q", "%" + q + "%");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta v = new Venta
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        NumeroFactura = datos.Lector["NumeroFactura"] != DBNull.Value
                            ? datos.Lector["NumeroFactura"].ToString()
                            : "",
                        MetodoPago = datos.Lector["MetodoPago"] != DBNull.Value
                            ? datos.Lector["MetodoPago"].ToString()
                            : "",
                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString()
                        },
                        TotalBD = datos.Lector["Total"] != DBNull.Value
                            ? Convert.ToDecimal(datos.Lector["Total"])
                            : 0
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
                        V.Total,
                        C.Id AS IdCliente,
                        C.Nombre AS NombreCliente
                    FROM Ventas V
                    INNER JOIN Clientes C ON V.IdCliente = C.Id
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
                        NumeroFactura = datos.Lector["NumeroFactura"].ToString(),
                        MetodoPago = datos.Lector["MetodoPago"].ToString(),
                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString()
                        },
                        Lineas = new List<VentaLinea>()
                    };
                }
            }
            finally { datos.CerrarConexion(); }

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

                int idVenta = Convert.ToInt32(datos.ejecutarScalar());

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
    }
}
