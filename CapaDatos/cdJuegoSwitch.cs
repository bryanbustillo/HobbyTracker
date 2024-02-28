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
    public class cdJuegoSwitch
    {
        //********************************************* Listar juegos switch *********************************************
        public List<eJuegoSwitch> listarJuegosSwitch(int IDCliente)
        {
            List<eJuegoSwitch> listaJuegosSwitch = new List<eJuegoSwitch>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarJuegosSwitch";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaJuegosSwitch.Add(
                                new eJuegoSwitch()
                                {
                                    IDJuegoSwitch = Convert.ToInt32(dr["IDJuegoSwitch"]),
                                    juego = dr["juego"].ToString(),
                                    //formato = Convert.ToBoolean(dr["formato"]),
                                    //progreso = Convert.ToBoolean(dr["progreso"]),
                                    formato = Convert.ToInt32(dr["formato"]),
                                    progreso = Convert.ToInt32(dr["progreso"]),
                                    estado = Convert.ToBoolean(dr["estado"]),
                                    observaciones = dr["observaciones"].ToString()
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
                listaJuegosSwitch = new List<eJuegoSwitch>();
            }

            return listaJuegosSwitch;
        }

        //********************************************* Agregar juegos switch *********************************************
        public int insertarJuegoSwitch(eJuegoSwitch obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarJuegoSwitch", oconexion);
                    cmd.Parameters.AddWithValue("juego", obj.juego);
                    cmd.Parameters.AddWithValue("formato", obj.formato);
                    cmd.Parameters.AddWithValue("progreso", obj.progreso);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    if (string.IsNullOrEmpty(obj.observaciones))
                    {
                        cmd.Parameters.AddWithValue("observaciones", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("observaciones", obj.observaciones);
                    }
                    cmd.Parameters.AddWithValue("IDCliente", obj.cliente);
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

        //********************************************* Editar juegos switch *********************************************
        public bool editarJuegoSwitch(eJuegoSwitch obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarJuegoSwitch", oconexion);
                    cmd.Parameters.AddWithValue("IDJuegoSwitch", obj.IDJuegoSwitch);
                    cmd.Parameters.AddWithValue("juego", obj.juego);
                    cmd.Parameters.AddWithValue("formato", obj.formato);
                    cmd.Parameters.AddWithValue("progreso", obj.progreso);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    if (string.IsNullOrEmpty(obj.observaciones))
                    {
                        cmd.Parameters.AddWithValue("observaciones", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("observaciones", obj.observaciones);
                    }
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

        //********************************************* Eliminar juegos switch *********************************************
        public bool eliminarJuegoSwitch(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarJuegoSwitch", oconexion);
                    cmd.Parameters.AddWithValue("@IDJuegoSwitch", id);
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

    }
}
