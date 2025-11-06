using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
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
            ORDER BY V.Fecha DESC
        ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta v = new Venta
                    {
                        Id = (int)datos.Lector["Id"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        NumeroFactura = datos.Lector["NumeroFactura"] != DBNull.Value ? datos.Lector["NumeroFactura"].ToString() : "",
                        MetodoPago = datos.Lector["MetodoPago"] != DBNull.Value ? datos.Lector["MetodoPago"].ToString() : "",
                        Cliente = new Cliente
                        {
                            Id = (int)datos.Lector["IdCliente"],
                            Nombre = datos.Lector["NombreCliente"].ToString()
                        },
                        TotalBD = datos.Lector["Total"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Total"]) : 0
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
                    FROM VentaLinea VL
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
    }
}


