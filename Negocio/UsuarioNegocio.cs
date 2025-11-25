using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool RegistrarUsuario(Usuario nuevoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (ExisteUsername(nuevoUsuario.Username))
                {
                    throw new Exception("El nombre de usuario ya está en uso");
                }

                if (ExisteEmail(nuevoUsuario.Email))
                {
                    throw new Exception("El correo electrónico ya está registrado");
                }

                datos.setearConsulta(@"INSERT INTO USUARIOS (Nombre, Documento, Email, Telefono, Direccion, Localidad, Username, Password, Activo) 
                                       VALUES (@Nombre, @Documento, @Email, @Telefono, @Direccion, @Localidad, @Username, @Password, @Activo);
                                       SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@Nombre", nuevoUsuario.Nombre);
                datos.setearParametro("@Documento", nuevoUsuario.Documento);
                datos.setearParametro("@Email", nuevoUsuario.Email);
                datos.setearParametro("@Telefono", nuevoUsuario.Telefono);
                datos.setearParametro("@Direccion", nuevoUsuario.Direccion);
                datos.setearParametro("@Localidad", nuevoUsuario.Localidad);
                datos.setearParametro("@Username", nuevoUsuario.Username);
                datos.setearParametro("@Password", nuevoUsuario.Password);
                datos.setearParametro("@Activo", nuevoUsuario.Activo);

                int idUsuario = Convert.ToInt32(datos.EjecutarScalar());

                // Asignar rol de Vendedor (Id = 2)
                AsignarRol(idUsuario, 2);

                // Mail de bienvenida si tiene email
                if (!string.IsNullOrWhiteSpace(nuevoUsuario.Email))
                {
                    EmailService.EnviarBienvenidaUsuario(nuevoUsuario);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool ExisteUsername(string username)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM USUARIOS WHERE Username = @Username");
                datos.setearParametro("@Username", username);

                int count = Convert.ToInt32(datos.EjecutarScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM USUARIOS WHERE Email = @Email");
                datos.setearParametro("@Email", email);

                int count = Convert.ToInt32(datos.EjecutarScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private void AsignarRol(int idUsuario, int idRol)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO USUARIO_ROLES (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol)");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdRol", idRol);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Usuario ValidarLogin(string username, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setearConsulta(@"SELECT U.Id, U.Nombre, U.Documento, U.Email, U.Telefono, 
                                      U.Direccion, U.Localidad, U.Username, U.Activo
                                      FROM USUARIOS U
                                      WHERE U.Username = @Username AND U.Password = @Password AND U.Activo = 1");

                datos.setearParametro("@Username", username);
                datos.setearParametro("@Password", password);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Documento = datos.Lector["Documento"] != DBNull.Value ? datos.Lector["Documento"].ToString() : null,
                        Email = datos.Lector["Email"].ToString(),
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : null,
                        Direccion = datos.Lector["Direccion"] != DBNull.Value ? datos.Lector["Direccion"].ToString() : null,
                        Localidad = datos.Lector["Localidad"] != DBNull.Value ? datos.Lector["Localidad"].ToString() : null,
                        Username = datos.Lector["Username"].ToString(),
                        Activo = (bool)datos.Lector["Activo"]
                    };

                    // Cargar roles del usuario
                    usuario.Roles = ObtenerRolesUsuario(usuario.Id);
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private List<UsuarioRol> ObtenerRolesUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<UsuarioRol> roles = new List<UsuarioRol>();

            try
            {
                datos.setearConsulta(@"SELECT R.Id, R.Descripcion 
                                      FROM ROLES R
                                      INNER JOIN USUARIO_ROLES UR ON R.Id = UR.IdRol
                                      WHERE UR.IdUsuario = @IdUsuario");

                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    UsuarioRol rol = new UsuarioRol
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Descripcion"].ToString()
                    };
                    roles.Add(rol);
                }

                return roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE USUARIOS 
                              SET Nombre = @Nombre, 
                                  Documento = @Documento, 
                                  Email = @Email, 
                                  Telefono = @Telefono, 
                                  Direccion = @Direccion, 
                                  Localidad = @Localidad
                              WHERE Id = @Id");

                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Documento", usuario.Documento);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Telefono", usuario.Telefono);
                datos.setearParametro("@Direccion", usuario.Direccion);
                datos.setearParametro("@Localidad", usuario.Localidad);
                datos.setearParametro("@Id", usuario.Id);

                datos.ejecutarAccion();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool CambiarPassword(int idUsuario, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Password = @Password WHERE Id = @Id");
                datos.setearParametro("@Password", nuevaPassword);
                datos.setearParametro("@Id", idUsuario);

                datos.ejecutarAccion();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
