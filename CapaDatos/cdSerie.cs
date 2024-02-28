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
    public class cdSerie
    {
        //********************************************* Listar series *********************************************
        public List<eSerie> listarSeries(int IDCliente)
        {
            List<eSerie> listaSeries = new List<eSerie>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarSeries";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaSeries.Add(
                                new eSerie()
                                {
                                    IDSerie = Convert.ToInt32(dr["IDSerie"]),
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
                listaSeries = new List<eSerie>();
            }

            return listaSeries;
        }

        //********************************************* Agregar series *********************************************
        public int insertarSerie(eSerie obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarSerie", oconexion);
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
                    //if (string.IsNullOrEmpty(obj.registro))
                    //{
                    //    cmd.Parameters.AddWithValue("registro", DBNull.Value);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("registro", obj.registro);
                    //}
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

        //********************************************* Editar series *********************************************
        public bool editarSerie(eSerie obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarSerie", oconexion);
                    cmd.Parameters.AddWithValue("IDSerie", obj.IDSerie);
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

        //********************************************* Eliminar series *********************************************
        public bool eliminarSerie(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarSerie", oconexion);
                    cmd.Parameters.AddWithValue("@IDSerie", id);
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
