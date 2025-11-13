using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar(string q = null)
        {
            var lista = new List<Marca>();
            var datos = new AccesoDatos();
            try
            {
                if (string.IsNullOrWhiteSpace(q))
                {
                    datos.setearConsulta("SELECT Id, Nombre FROM Marcas ORDER BY Nombre");
                }
                else
                {
                    datos.setearConsulta("SELECT Id, Nombre FROM Marcas WHERE Nombre LIKE @q ORDER BY Nombre");
                    datos.setearParametro("@q", "%" + q + "%");
                }

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new Marca { Id = (int)datos.Lector["Id"], Nombre = (string)datos.Lector["Nombre"] });
                }
                return lista;
            }
            finally { datos.CerrarConexion(); }
        }

        public void Agregar(Marca marca)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Nombre) VALUES (@nombre)");
                datos.setearParametro("@nombre", marca.Nombre);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Modificar(Marca marca)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Marcas SET Nombre=@nombre WHERE Id=@id");
                datos.setearParametro("@nombre", marca.Nombre);
                datos.setearParametro("@id", marca.Id);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Verificar si tiene productos asociados
                datos.setearConsulta("SELECT COUNT(*) FROM PRODUCTOS WHERE IdMarca = @id AND Activo = 1");
                datos.setearParametro("@id", id);

                int cantidad = Convert.ToInt32(datos.ejecutarScalar());

                if (cantidad > 0)
                {
                    throw new Exception("No se puede eliminar la marca porque tiene productos asociados.");
                }

                // Si no tiene productos, eliminar
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id");
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
