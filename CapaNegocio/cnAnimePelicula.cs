using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnAnimePelicula
    {
        private cdAnimePelicula objCapaDatos = new cdAnimePelicula();

        //********************************************* Listar animes películas *********************************************
        public List<eAnimePelicula> listarAnimePeliculas(int IDCliente)
        {
            return objCapaDatos.listarAnimePeliculas(IDCliente);
        }

        //********************************************* Agregar animes películas *********************************************
        public int insertarAnimePelicula(eAnimePelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la película de anime no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarAnimePelicula(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar animes películas *********************************************
        public bool editarAnimePelicula(eAnimePelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la película de anime no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarAnimePelicula(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar animes películas *********************************************
        public bool eliminarAnimePelicula(int id, out string mensaje)
        {
            return objCapaDatos.eliminarAnimePelicula(id, out mensaje);
        }

    }
}
