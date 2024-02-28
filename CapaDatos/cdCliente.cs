using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaDatos
{
    public class cdCliente
    {
        //********************************************* Listar clientes *********************************************
        public List<eCliente> ListarClientes()
        {
            List<eCliente> listaClientes = new List<eCliente>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarClientes";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaClientes.Add(
                                new eCliente()
                                {
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    nombre = dr["nombre"].ToString(),
                                    apellido = dr["apellido"].ToString(),
                                    email = dr["email"].ToString(),
                                    contrasena = dr["contrasena"].ToString(),
                                    restablecerContrasena = Convert.ToBoolean(dr["restablecerContrasena"])
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine(ex.Message);
                listaClientes = new List<eCliente>();
            }

            return listaClientes;
        }

        //********************************************* Listar clientes perfil *********************************************
        public List<eCliente> ListarClientesPerfil(int IDCliente)
        {
            List<eCliente> listaClientes = new List<eCliente>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarClientesPerfil";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaClientes.Add(
                                new eCliente()
                                {
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    nombre = dr["nombre"].ToString(),
                                    apellido = dr["apellido"].ToString(),
                                    email = dr["email"].ToString(),
                                    contrasena = dr["contrasena"].ToString(),
                                    restablecerContrasena = Convert.ToBoolean(dr["restablecerContrasena"])
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine(ex.Message);
                listaClientes = new List<eCliente>();
            }

            return listaClientes;
        }

        //********************************************* Agregar clientes *********************************************
        public int insertarCliente(eCliente obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarCliente", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("contrasena", obj.contrasena);
                    cmd.Parameters.AddWithValue("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = ex.Message;
            }

            return idAutogenerado;
        }

        //********************************************* Editar libros *********************************************
        public bool editarCliente(eCliente obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarCliente", oconexion);
                    cmd.Parameters.AddWithValue("IDCliente", obj.IDCliente);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        //********************************************* Cambiar contraseña perfil *********************************************

        public bool cambiarContrasenaPerfil(int IDCliente, string nuevaContrasena, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_CambiarContrasenaPerfil", oconexion);
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);
                    cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        //********************************************* Cambiar contraseña *********************************************
        public bool cambiarContrasena(int IDCliente, string nuevaContrasena, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_CambiarContrasena", oconexion);
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);
                    cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        //********************************************* Restablecer contraseña *********************************************
        public bool restablecerContrasena(int IDCliente, string nuevaContrasena, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_RestablecerContrasaena", oconexion);
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);
                    cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    cmd.Parameters.AddWithValue("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }

    }
}
