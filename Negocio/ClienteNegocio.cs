using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar(string q = null)
        {
            var lista = new List<Cliente>();
            var datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT Id, Nombre, Documento, Email, Telefono, Direccion,
                   Localidad, CondicionIVA, Habilitado, IdUsuarioAlta
            FROM Clientes
            WHERE Habilitado = 1";

                if (!string.IsNullOrWhiteSpace(q))
                {
                    consulta += @"
                AND (Nombre LIKE @q 
                     OR Documento LIKE @q 
                     OR Email LIKE @q 
                     OR Telefono LIKE @q 
                     OR Localidad LIKE @q)";
                }

                consulta += " ORDER BY Nombre";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(q))
                    datos.setearParametro("@q", "%" + q + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var c = new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Documento = datos.Lector["Documento"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        Direccion = datos.Lector["Direccion"].ToString(),
                        Localidad = datos.Lector["Localidad"].ToString(),
                        CondicionIVA = datos.Lector["CondicionIVA"].ToString(),
                        Habilitado = (bool)datos.Lector["Habilitado"],

                        IdUsuarioAlta = datos.Lector["IdUsuarioAlta"] != DBNull.Value
                                        ? Convert.ToInt32(datos.Lector["IdUsuarioAlta"])
                                        : 0
                    };

                    lista.Add(c);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



        public Cliente BuscarPorId(int id)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado FROM Clientes WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Documento = datos.Lector["Documento"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        Direccion = datos.Lector["Direccion"].ToString(),
                        Localidad = datos.Lector["Localidad"].ToString(),
                        CondicionIVA = datos.Lector["CondicionIVA"].ToString(),
                        Habilitado = (bool)datos.Lector["Habilitado"]
                    };
                }
                return null;
            }
            finally { datos.CerrarConexion(); }
        }

        public void Guardar(Cliente c)
        {
            var datos = new AccesoDatos();

            try
            {
                if (c.Id == 0)
                {
                    datos.setearConsulta(@"
                INSERT INTO Clientes 
                (Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado, IdUsuarioAlta)
                VALUES 
                (@nom, @doc, @email, @tel, @dir, @loc, @iva, @hab, @idUser)
            ");
                }
                else
                {
                    datos.setearConsulta(@"
                UPDATE Clientes SET
                Nombre=@nom, Documento=@doc, Email=@email, Telefono=@tel, Direccion=@dir, 
                Localidad=@loc, CondicionIVA=@iva, Habilitado=@hab
                WHERE Id=@id
            ");

                    datos.setearParametro("@id", c.Id);
                }

                datos.setearParametro("@nom", c.Nombre);
                datos.setearParametro("@doc", c.Documento);
                datos.setearParametro("@email", c.Email);
                datos.setearParametro("@tel", c.Telefono);
                datos.setearParametro("@dir", c.Direccion);
                datos.setearParametro("@loc", c.Localidad);
                datos.setearParametro("@iva", c.CondicionIVA);
                datos.setearParametro("@hab", c.Habilitado);

                if (c.Id == 0)
                    datos.setearParametro("@idUser", c.IdUsuarioAlta);

                datos.ejecutarAccion();
            }
            catch (SqlException ex)
            {
                // Error 2601 o 2627 = violación UNIQUE
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    throw new Exception("El documento ingresado ya existe. Por favor verifique los datos.");
                }

                throw; 
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
                datos.setearConsulta("UPDATE CLIENTES SET Habilitado = 0 WHERE Id = @id");
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
