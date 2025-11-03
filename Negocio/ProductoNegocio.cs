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
                datos.setearConsulta("SELECT Id, CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, UrlImagen, Activo FROM Productos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto p = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        CodigoSKU = (string)datos.Lector["CodigoSKU"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        StockMinimo = (decimal)datos.Lector["StockMinimo"],
                        StockActual = (decimal)datos.Lector["StockActual"],
                        PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"],
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? "" : (string)datos.Lector["UrlImagen"],
                        Activo = datos.Lector["Activo"] is DBNull ? false : (bool)datos.Lector["Activo"]
                    };
                    lista.Add(p);
                }
                return lista;
            }
            finally { datos.CerrarConexion(); }
        }

        public Producto ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM Productos WHERE Id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Producto p = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        CodigoSKU = (string)datos.Lector["CodigoSKU"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        StockMinimo = (decimal)datos.Lector["StockMinimo"],
                        StockActual = (decimal)datos.Lector["StockActual"],
                        PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"],
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? "" : (string)datos.Lector["UrlImagen"],
                        Activo = datos.Lector["Activo"] is DBNull ? false : (bool)datos.Lector["Activo"]
                    };
                    return p;
                }
                return null;
            }
            finally { datos.CerrarConexion(); }
        }

        public void Guardar(Producto p)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (p.Id == 0)
                    datos.setearConsulta("INSERT INTO Productos (CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, UrlImagen, Activo) VALUES (@sku, @desc, @min, @act, @gan, @img, @actv)");
                else
                {
                    datos.setearConsulta("UPDATE Productos SET CodigoSKU=@sku, Descripcion=@desc, StockMinimo=@min, StockActual=@act, PorcentajeGanancia=@gan, UrlImagen=@img, Activo=@actv WHERE Id=@id");
                    datos.setearParametro("@id", p.Id);
                }

                datos.setearParametro("@sku", p.CodigoSKU);
                datos.setearParametro("@desc", p.Descripcion);
                datos.setearParametro("@min", p.StockMinimo);
                datos.setearParametro("@act", p.StockActual);
                datos.setearParametro("@gan", p.PorcentajeGanancia);
                datos.setearParametro("@img", p.UrlImagen);
                datos.setearParametro("@actv", p.Activo);

                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Productos WHERE Id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }
    }
}
