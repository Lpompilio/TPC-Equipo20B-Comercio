using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar(string q = null)
        {
            var lista = new List<Proveedor>();
            var datos = new AccesoDatos();

            try
            {
                string consulta = @"
                    SELECT Id, Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Activo
                    FROM PROVEEDORES
                    WHERE Activo = 1";

                if (!string.IsNullOrWhiteSpace(q))
                    consulta += " AND (Nombre LIKE @q OR RazonSocial LIKE @q OR Documento LIKE @q OR Email LIKE @q OR Telefono LIKE @q OR Localidad LIKE @q)";

                consulta += " ORDER BY Nombre";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(q))
                    datos.setearParametro("@q", "%" + q + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var p = new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"] as string ?? "",
                        RazonSocial = datos.Lector["RazonSocial"] as string ?? "",
                        Documento = datos.Lector["Documento"] as string ?? "",
                        Email = datos.Lector["Email"] as string ?? "",
                        Telefono = datos.Lector["Telefono"] as string ?? "",
                        Direccion = datos.Lector["Direccion"] as string ?? "",
                        Localidad = datos.Lector["Localidad"] as string ?? "",
                        CondicionIVA = datos.Lector["CondicionIVA"] as string ?? "",
                        Activo = datos.Lector["Activo"] != DBNull.Value && (bool)datos.Lector["Activo"]
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

        public Proveedor ObtenerPorId(int id)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT Id, Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Activo
                    FROM PROVEEDORES
                    WHERE Id = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Proveedor p = null;

                if (datos.Lector.Read())
                {
                    p = new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"] as string ?? "",
                        RazonSocial = datos.Lector["RazonSocial"] as string ?? "",
                        Documento = datos.Lector["Documento"] as string ?? "",
                        Email = datos.Lector["Email"] as string ?? "",
                        Telefono = datos.Lector["Telefono"] as string ?? "",
                        Direccion = datos.Lector["Direccion"] as string ?? "",
                        Localidad = datos.Lector["Localidad"] as string ?? "",
                        CondicionIVA = datos.Lector["CondicionIVA"] as string ?? "",
                        Activo = datos.Lector["Activo"] != DBNull.Value && (bool)datos.Lector["Activo"]
                    };
                }

                return p;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // Wrapper para mantener compatibilidad con el código de la web
        public Proveedor BuscarPorId(int id)
        {
            return ObtenerPorId(id);
        }

        public void Guardar(Proveedor p)
        {
            var datos = new AccesoDatos();

            try
            {
                // Busco si existe un proveedor con este CUIT
                var existente = BuscarPorCuit(p.Documento);

                // Si existe y NO soy yo
                if (existente != null && existente.Id != p.Id)
                {
                    if (existente.Activo)
                    {
                        // Ya existe uno activo, no se puede duplicar
                        throw new Exception("Ya existe un proveedor activo con este CUIT.");
                    }
                    else
                    {
                        // Existe pero está INACTIVO, lo IGNORAMOS
                    }
                }

                // Alta nueva
                if (p.Id == 0)
                {
                    datos.setearConsulta(@"
                INSERT INTO PROVEEDORES
                    (Nombre, RazonSocial, Documento, Email, Telefono, Direccion, Localidad, CondicionIVA, Activo)
                VALUES
                    (@nom, @razon, @doc, @mail, @tel, @dir, @loc, @iva, 1)");
                }
                else
                {
                    // Modificación de un proveedor existente
                    datos.setearConsulta(@"
                UPDATE PROVEEDORES SET
                    Nombre = @nom,
                    RazonSocial = @razon,
                    Documento = @doc,
                    Email = @mail,
                    Telefono = @tel,
                    Direccion = @dir,
                    Localidad = @loc,
                    CondicionIVA = @iva
                WHERE Id = @id");

                    datos.setearParametro("@id", p.Id);
                }

                // Parámetros comunes
                datos.setearParametro("@nom", p.Nombre);
                datos.setearParametro("@razon", p.RazonSocial);
                datos.setearParametro("@doc", (object)p.Documento ?? DBNull.Value);
                datos.setearParametro("@mail", (object)p.Email ?? DBNull.Value);
                datos.setearParametro("@tel", (object)p.Telefono ?? DBNull.Value);
                datos.setearParametro("@dir", (object)p.Direccion ?? DBNull.Value);
                datos.setearParametro("@loc", (object)p.Localidad ?? DBNull.Value);
                datos.setearParametro("@iva", (object)p.CondicionIVA ?? DBNull.Value);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



        public void Eliminar(int id)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PROVEEDORES SET Activo = 0 WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Proveedor BuscarPorCuit(string cuit)
        {
            var datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM PROVEEDORES WHERE Documento = @cuit");
                datos.setearParametro("@cuit", cuit);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        RazonSocial = datos.Lector["RazonSocial"].ToString(),
                        Documento = datos.Lector["Documento"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"].ToString(),
                        Direccion = datos.Lector["Direccion"].ToString(),
                        Localidad = datos.Lector["Localidad"].ToString(),
                        CondicionIVA = datos.Lector["CondicionIVA"].ToString(),
                        Activo = (bool)datos.Lector["Activo"]
                    };
                }

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
