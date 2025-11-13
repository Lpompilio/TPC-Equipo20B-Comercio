using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar(string filtro = null)
        {
            var lista = new List<Categoria>();
            var datos = new AccesoDatos();
            try
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearConsulta("SELECT Id, Nombre FROM Categorias ORDER BY Nombre");
                }
                else
                {
                    datos.setearConsulta(@"SELECT Id, Nombre 
                                           FROM Categorias 
                                           WHERE Nombre LIKE @filtro
                                           ORDER BY Nombre");
                    datos.setearParametro("@filtro", $"%{filtro}%");
                }

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new Categoria
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"]
                    });
                }
                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Categoria cat)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Categorias (Nombre) VALUES (@nombre)");
                datos.setearParametro("@nombre", cat.Nombre);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Modificar(Categoria cat)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Categorias SET Nombre=@nombre WHERE Id=@id");
                datos.setearParametro("@nombre", cat.Nombre);
                datos.setearParametro("@id", cat.Id);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Verificar si hay productos asociados a esta categoría
                datos.setearConsulta("SELECT COUNT(*) FROM PRODUCTOS WHERE IdCategoria = @id AND Activo = 1");
                datos.setearParametro("@id", id);

                int cantidad = Convert.ToInt32(datos.ejecutarScalar());

                if (cantidad > 0)
                {
                    throw new Exception("No se puede eliminar la categoría porque tiene productos asociados.");
                }

                // Si no tiene productos, eliminar
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public Categoria ObtenerPorId(int id)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre FROM Categorias WHERE Id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Categoria
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"]
                    };
                }
                return null;
            }
            finally { datos.CerrarConexion(); }
        }
    }
}
