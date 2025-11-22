using System;
using System.Collections.Generic;
using Dominio;

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
                if (string.IsNullOrWhiteSpace(q))
                {
                    datos.setearConsulta(@"
                SELECT Id, Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado
                FROM Clientes
                WHERE Habilitado = 1
                ORDER BY Nombre");
                }
                else
                {
                    datos.setearConsulta(@"
                SELECT Id, Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado
                FROM Clientes
                WHERE Habilitado = 1
                  AND (Nombre LIKE @q 
                       OR Documento LIKE @q 
                       OR Email LIKE @q 
                       OR Telefono LIKE @q 
                       OR Localidad LIKE @q)
                ORDER BY Nombre");

                    datos.setearParametro("@q", "%" + q + "%");
                }

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
                        Habilitado = (bool)datos.Lector["Habilitado"]
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
                        INSERT INTO Clientes (Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado)
                        VALUES (@nom, @doc, @email, @tel, @dir, @loc, @iva, @hab)
                    ");
                }
                else
                {
                    datos.setearConsulta(@"
                        UPDATE Clientes SET
                        Nombre=@nom, Documento=@doc, Email=@email, Telefono=@tel, Direccion=@dir, Localidad=@loc, CondicionIVA=@iva, Habilitado=@hab
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
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
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
