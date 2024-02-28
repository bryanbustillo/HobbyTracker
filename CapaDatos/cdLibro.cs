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
    public class cdLibro
    {
        //********************************************* Listar libros *********************************************
        public List<eLibro> listarLibros(int IDCliente)
        {
            List<eLibro> listaLibros = new List<eLibro>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarLibros";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaLibros.Add(
                                new eLibro()
                                {
                                    IDLibro = Convert.ToInt32(dr["IDLibro"]),
                                    nombre = dr["nombre"].ToString(),
                                    autor = dr["autor"].ToString(),
                                    editorial = dr["editorial"].ToString(),
                                    leido = Convert.ToBoolean(dr["leido"])
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
                listaLibros = new List<eLibro>();
            }

            return listaLibros;
        }

        //********************************************* Agregar libros *********************************************
        public int insertarLibro(eLibro obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarLibro", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    //cmd.Parameters.AddWithValue("autor", obj.autor);
                    if (string.IsNullOrEmpty(obj.autor))
                    {
                        cmd.Parameters.AddWithValue("autor", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("autor", obj.autor);
                    }
                    //cmd.Parameters.AddWithValue("editorial", obj.editorial);
                    if (string.IsNullOrEmpty(obj.editorial))
                    {
                        cmd.Parameters.AddWithValue("editorial", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("editorial", obj.editorial);
                    }
                    cmd.Parameters.AddWithValue("leido", obj.leido);
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

        //********************************************* Editar libros *********************************************
        public bool editarLibro(eLibro obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarLibro", oconexion);
                    cmd.Parameters.AddWithValue("IDLibro", obj.IDLibro);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    //cmd.Parameters.AddWithValue("autor", obj.autor);
                    if (string.IsNullOrEmpty(obj.autor))
                    {
                        cmd.Parameters.AddWithValue("autor", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("autor", obj.autor);
                    }
                    //cmd.Parameters.AddWithValue("editorial", obj.editorial);
                    if (string.IsNullOrEmpty(obj.editorial))
                    {
                        cmd.Parameters.AddWithValue("editorial", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("editorial", obj.editorial);
                    }
                    cmd.Parameters.AddWithValue("leido", obj.leido);
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

        //********************************************* Eliminar libros *********************************************
        public bool eliminarLibro(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarLibro", oconexion);
                    cmd.Parameters.AddWithValue("@IDLibro", id);
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
