using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA FROM Proveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor p = new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        RazonSocial = datos.Lector["RazonSocial"] != DBNull.Value ? datos.Lector["RazonSocial"].ToString() : "",
                        Documento = datos.Lector["Documento"] != DBNull.Value ? datos.Lector["Documento"].ToString() : "",
                        Email = datos.Lector["Email"] != DBNull.Value ? datos.Lector["Email"].ToString() : "",
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "",
                        Direccion = datos.Lector["Direccion"] != DBNull.Value ? datos.Lector["Direccion"].ToString() : "",
                        Localidad = datos.Lector["Localidad"] != DBNull.Value ? datos.Lector["Localidad"].ToString() : "",
                        CondicionIVA = datos.Lector["CondicionIVA"] != DBNull.Value ? datos.Lector["CondicionIVA"].ToString() : ""
                    };

                    lista.Add(p);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Proveedor BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA FROM Proveedores WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        RazonSocial = datos.Lector["RazonSocial"] != DBNull.Value ? datos.Lector["RazonSocial"].ToString() : "",
                        Documento = datos.Lector["Documento"] != DBNull.Value ? datos.Lector["Documento"].ToString() : "",
                        Email = datos.Lector["Email"] != DBNull.Value ? datos.Lector["Email"].ToString() : "",
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "",
                        Direccion = datos.Lector["Direccion"] != DBNull.Value ? datos.Lector["Direccion"].ToString() : "",
                        Localidad = datos.Lector["Localidad"] != DBNull.Value ? datos.Lector["Localidad"].ToString() : "",
                        CondicionIVA = datos.Lector["CondicionIVA"] != DBNull.Value ? datos.Lector["CondicionIVA"].ToString() : ""
                    };
                }

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Guardar(Proveedor p)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (p.Id == 0)
                    datos.setearConsulta(@"INSERT INTO Proveedores 
                        (Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA)
                        VALUES (@nombre, @razon, @doc, @email, @tel, @dir, @loc, @iva)");
                else
                {
                    datos.setearConsulta(@"UPDATE Proveedores SET 
                        Nombre = @nombre, 
                        RazonSocial = @razon,
                        Documento = @doc,
                        Email = @email,
                        Telefono = @tel,
                        Direccion = @dir,
                        Localidad = @loc,
                        CondicionIVA = @iva
                        WHERE Id = @id");
                    datos.setearParametro("@id", p.Id);
                }

                datos.setearParametro("@nombre", p.Nombre);
                datos.setearParametro("@razon", p.RazonSocial);
                datos.setearParametro("@doc", p.Documento);
                datos.setearParametro("@email", p.Email);
                datos.setearParametro("@tel", p.Telefono);
                datos.setearParametro("@dir", p.Direccion);
                datos.setearParametro("@loc", p.Localidad);
                datos.setearParametro("@iva", p.CondicionIVA);

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
                datos.setearConsulta("DELETE FROM Proveedores WHERE Id = @id");
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

