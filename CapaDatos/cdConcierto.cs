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
    public class cdConcierto
    {
        //********************************************* Listar conciertos *********************************************
        public List<eConcierto> listarConciertos(int IDCliente)
        {
            List<eConcierto> listaConciertos = new List<eConcierto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarConciertos";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaConciertos.Add(
                                new eConcierto()
                                {
                                    IDConcierto = Convert.ToInt32(dr["IDConcierto"]),
                                    concierto = dr["concierto"].ToString(),
                                    lugar = dr["lugar"].ToString(),
                                    //fecha = dr["fecha"].ToString(),
                                    fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd/MM/yyyy"),
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
                listaConciertos = new List<eConcierto>();
            }

            return listaConciertos;
        }

        //********************************************* Agregar conciertos *********************************************
        public int insertarConcierto(eConcierto obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarConcierto", oconexion);
                    cmd.Parameters.AddWithValue("concierto", obj.concierto);
                    cmd.Parameters.AddWithValue("lugar", obj.lugar);
                    DateTime fecha = DateTime.Parse(obj.fecha);
                    cmd.Parameters.AddWithValue("fecha", fecha);
                    //cmd.Parameters.AddWithValue("estado", obj.estado);
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

        //********************************************* Editar conciertos *********************************************
        public bool editarConcierto(eConcierto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarConcierto", oconexion);
                    cmd.Parameters.AddWithValue("IDConcierto", obj.IDConcierto);
                    cmd.Parameters.AddWithValue("concierto", obj.concierto);
                    cmd.Parameters.AddWithValue("lugar", obj.lugar);
                    DateTime fecha = DateTime.Parse(obj.fecha);
                    string fechaFormateada = fecha.ToString("yyyy/MM/dd"); // Convertir la fecha al formato deseado
                    cmd.Parameters.AddWithValue("fecha", fechaFormateada);                    
                    //cmd.Parameters.AddWithValue("estado", obj.estado);
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

        //********************************************* Eliminar conciertos *********************************************
        public bool eliminarConcierto(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarConcierto", oconexion);
                    cmd.Parameters.AddWithValue("@IDConcierto", id);
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
