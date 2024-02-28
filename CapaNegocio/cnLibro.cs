using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnLibro
    {
        private cdLibro objCapaDatos = new cdLibro();

        //********************************************* Listar libros *********************************************
        public List<eLibro> listarLibros(int IDCliente)
        {
            return objCapaDatos.listarLibros(IDCliente);
        }

        //********************************************* Agregar libros *********************************************
        public int insertarLibro(eLibro obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del libro no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarLibro(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar libros *********************************************
        public bool editarLibro(eLibro obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del libro no puede estar vació.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarLibro(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar libros *********************************************
        public bool eliminarLibro(int id, out string mensaje)
        {
            return objCapaDatos.eliminarLibro(id, out mensaje);
        }

    }
}
