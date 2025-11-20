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
                    datos.setearConsulta(@"
                SELECT Id, Nombre 
                FROM Categorias 
                WHERE Activo = 1
                ORDER BY Nombre");
                }
                else
                {
                    datos.setearConsulta(@"
                SELECT Id, Nombre 
                FROM Categorias 
                WHERE Activo = 1 AND Nombre LIKE @filtro
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
                datos.setearConsulta("UPDATE CATEGORIAS SET Activo = 0 WHERE Id = @id");
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
