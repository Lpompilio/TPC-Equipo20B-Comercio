using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {

        public List<Producto> Listar()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, CodigoSKU, Descripcion, StockMinimo, StockActual, PorcentajeGanancia, UrlImagen, Activo from Productos;");
                datos.ejecutarLectura();

                List<Producto> lista = new List<Producto>();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoSKU = (string)datos.Lector["CodigoSKU"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.StockMinimo = (decimal)datos.Lector["StockMinimo"];
                    aux.StockActual = (decimal)datos.Lector["StockActual"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    if (!(datos.Lector["Activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Marca = null;
                    aux.Categoria = null;
                    aux.Proveedor = null;

                    lista.Add(aux);
                }


                return lista;

            }

            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
