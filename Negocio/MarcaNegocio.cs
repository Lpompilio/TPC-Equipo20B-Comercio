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
                string consultaBase = @"
            SELECT Id, Nombre
            FROM Marcas
            WHERE Activo = 1";

                if (string.IsNullOrWhiteSpace(q))
                {
                    datos.setearConsulta(consultaBase + " ORDER BY Nombre");
                }
                else
                {
                    datos.setearConsulta(consultaBase + @"
                AND Nombre LIKE @q
                ORDER BY Nombre");
                    datos.setearParametro("@q", "%" + q + "%");
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Marca
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    });
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
                datos.setearConsulta("UPDATE MARCAS SET Activo = 0 WHERE Id = @id");
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
