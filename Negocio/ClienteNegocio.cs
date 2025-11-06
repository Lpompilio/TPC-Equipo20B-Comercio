using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado FROM Clientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente c = new Cliente
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
            finally { datos.CerrarConexion(); }
        }

        public Cliente BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

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
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (c.Id == 0)
                {
                    datos.setearConsulta(@"INSERT INTO Clientes 
                        (Nombre, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Habilitado) 
                        VALUES (@nom, @doc, @email, @tel, @dir, @loc, @iva, @hab)");
                }
                else
                {
                    datos.setearConsulta(@"UPDATE Clientes SET 
                        Nombre = @nom, Documento = @doc, Email = @email, 
                        Telefono = @tel, Direccion = @dir, Localidad = @loc, 
                        CondicionIVA = @iva, Habilitado = @hab
                        WHERE Id = @id");
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
                datos.setearConsulta("DELETE FROM Clientes WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally { datos.CerrarConexion(); }
        }
    }
}

