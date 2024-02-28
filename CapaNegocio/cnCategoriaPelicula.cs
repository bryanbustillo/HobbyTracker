using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnCategoriaPelicula
    {
        private cdCategoriaPelicula objCapaDatos = new cdCategoriaPelicula();

        //********************************************* Listar categorías *********************************************
        public List<eCategoriaPelicula> listarCategorias(int IDCliente)
        {
            return objCapaDatos.listarCategorias(IDCliente);
        }

        //********************************************* Agregar categorías *********************************************
        public int insertarCategoria(eCategoriaPelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripción de la categoría no puede estar vacía.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarCategoria(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar categorías *********************************************
        public bool editarCategoria(eCategoriaPelicula obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripción de la categoría no puede estar vacía.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarCategoria(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar categorías *********************************************
        public bool eliminarCategoria(int id, out string mensaje)
        {
            return objCapaDatos.eliminarCategoria(id, out mensaje);
        }
    }
}
