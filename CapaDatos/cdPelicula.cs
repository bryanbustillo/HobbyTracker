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
    public class cdPelicula
    {
        //********************************************* Listar peliculas *********************************************
        public List<ePelicula> listarPeliculas(int IDCliente)
        {
            List<ePelicula> listaPeliculas = new List<ePelicula>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    string storedProcedure = "SP_ListarPeliculas";

                    SqlCommand cmd = new SqlCommand(storedProcedure, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro IDCliente al procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDCliente", IDCliente);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaPeliculas.Add(
                                new ePelicula()
                                {
                                    IDPelicula = Convert.ToInt32(dr["IDPelicula"]),
                                    nombre = dr["nombre"].ToString(),
                                    oCategoria = new eCategoriaPelicula() { IDCategoria = Convert.ToInt32(dr["IDCategoria"]), descripcion = dr["descripcion"].ToString() },
                                    vista = Convert.ToBoolean(dr["vista"]),
                                    observaciones = dr["observaciones"].ToString(),
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
                listaPeliculas = new List<ePelicula>();
            }

            return listaPeliculas;
        }

        //********************************************* Agregar peliculas *********************************************
        public int insertarPelicula(ePelicula obj, out string mensaje)
        {
            int idAutogenerado = 0;

            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertarPelicula", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    //cmd.Parameters.AddWithValue("IDCategoria", obj.IDCategoria);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.oCategoria.IDCategoria);
                    cmd.Parameters.AddWithValue("vista", obj.vista);
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

        //********************************************* Editar peliculas *********************************************
        public bool editarPelicula(ePelicula obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarPelicula", oconexion);
                    cmd.Parameters.AddWithValue("IDPelicula", obj.IDPelicula);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    //cmd.Parameters.AddWithValue("IDCategoria", obj.IDCategoria);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.oCategoria.IDCategoria);
                    cmd.Parameters.AddWithValue("vista", obj.vista);
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

        //********************************************* Eliminar películas *********************************************
        public bool eliminarPelicula(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(cdConexion.conexionBD))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarPelicula", oconexion);
                    cmd.Parameters.AddWithValue("@IDPelicula", id);
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
