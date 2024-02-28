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
    public class cdAnime
    {
        //********************************************* Listar animes *********************************************
        public List<eAnime> listarAnimes(int IDCliente)
        {
            List<eAnime> listaAnimes = new List<eAnime>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarAnimes";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaAnimes.Add(
                                new eAnime()
                                {
                                    IDAnime = Convert.ToInt32(dr["IDAnime"]),
                                    nombre = dr["nombre"].ToString(),
                                    estado = Convert.ToBoolean(dr["estado"]),
                                    registro = dr["registro"].ToString()
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
                listaAnimes = new List<eAnime>();
            }

            return listaAnimes;
        }

        //********************************************* Agregar animes *********************************************
        public int insertarAnime(eAnime obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarAnime", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    if (obj.estado)
                    {
                        cmd.Parameters.AddWithValue("registro", DBNull.Value);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(obj.registro))
                        {
                            cmd.Parameters.AddWithValue("registro", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("registro", obj.registro);
                        }
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

        //********************************************* Editar animes *********************************************
        public bool editarAnime(eAnime obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarAnime", oconexion);
                    cmd.Parameters.AddWithValue("IDAnime", obj.IDAnime);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("estado", obj.estado);

                    if (obj.estado)
                    {
                        cmd.Parameters.AddWithValue("registro", DBNull.Value);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(obj.registro))
                        {
                            cmd.Parameters.AddWithValue("registro", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("registro", obj.registro);
                        }
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

        //********************************************* Eliminar animes *********************************************
        public bool eliminarAnime(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarAnime", oconexion);
                    cmd.Parameters.AddWithValue("@IDAnime", id);
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
