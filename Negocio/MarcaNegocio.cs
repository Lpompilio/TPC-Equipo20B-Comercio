using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre FROM Marcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca m = new Marca();
                    m.Id = (int)datos.Lector["Id"];
                    m.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(m);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Agregar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Nombre) VALUES (@nombre)");
                datos.setearParametro("@nombre", marca.Nombre);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Marcas SET Nombre = @nombre WHERE Id = @id");
                datos.setearParametro("@nombre", marca.Nombre);
                datos.setearParametro("@id", marca.Id);
                datos.ejecutarAccion();
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
                datos.setearConsulta("DELETE FROM Marcas WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
