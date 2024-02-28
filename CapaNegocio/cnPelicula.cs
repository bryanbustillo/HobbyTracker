using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnPelicula
    {
        private cdPelicula objCapaDatos = new cdPelicula();

        //********************************************* Listar peliculas *********************************************
        public List<ePelicula> listarPeliculas(int IDCliente)
        {
            return objCapaDatos.listarPeliculas(IDCliente);
        }

        //********************************************* Agregar pelicula *********************************************
        public int insertarPelicula(ePelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la película no puede estar vacío.";
            }

            //if (obj.oCategoria.IDCategoria == 0)
            //if (obj.IDCategoria == 0)
            //{
            //    mensaje = "Debe seleccionar una categoría.";
            //}

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarPelicula(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar pelicula *********************************************
        public bool editarPelicula(ePelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la película no puede estar vacío.";
            }

            //if (obj.IDCategoria == 0)
            //{
            //    mensaje = "Debe seleccionar una categoría.";
            //}

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarPelicula(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar pelicula *********************************************
        public bool eliminarPelicula(int id, out string mensaje)
        {
            return objCapaDatos.eliminarPelicula(id, out mensaje);
        }

    }
}
