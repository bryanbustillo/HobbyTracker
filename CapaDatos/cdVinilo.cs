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
    public class cdVinilo
    {
        //********************************************* Listar vinilos *********************************************
        public List<eVinilo> listarVinilos(int IDCliente)
        {
            List<eVinilo> listaVinilos = new List<eVinilo>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarVinilos";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaVinilos.Add(
                                new eVinilo()
                                {
                                    IDVinilo = Convert.ToInt32(dr["IDVinilo"]),
                                    vinilo = dr["vinilo"].ToString(),
                                    banda = dr["banda"].ToString(),
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
                listaVinilos = new List<eVinilo>();
            }

            return listaVinilos;
        }

        //********************************************* Agregar vinilos *********************************************
        public int insertarVinilo(eVinilo obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarVinilo", oconexion);
                    cmd.Parameters.AddWithValue("vinilo", obj.vinilo);
                    cmd.Parameters.AddWithValue("banda", obj.banda);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    // Validar si el parámetro observaciones viene vacío
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

        //********************************************* Editar vinilos *********************************************
        public bool editarVinilo(eVinilo obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarVinilo", oconexion);
                    cmd.Parameters.AddWithValue("IDVinilo", obj.IDVinilo);
                    cmd.Parameters.AddWithValue("vinilo", obj.vinilo);
                    cmd.Parameters.AddWithValue("banda", obj.banda);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    //cmd.Parameters.AddWithValue("observaciones", obj.observaciones);               
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

        //********************************************* Eliminar vinilos *********************************************
        public bool eliminarVinilo(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarVinilo", oconexion);
                    cmd.Parameters.AddWithValue("@IDVinilo", id);
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
