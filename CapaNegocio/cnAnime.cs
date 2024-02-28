using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnAnime
    {
        private cdAnime objCapaDatos = new cdAnime();

        //********************************************* Listar animes *********************************************
        public List<eAnime> listarAnimes(int IDCliente)
        {
            return objCapaDatos.listarAnimes(IDCliente);
        }

        //********************************************* Agregar animes *********************************************
        public int insertarAnime(eAnime obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del anime no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarAnime(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar animes *********************************************
        public bool editarAnime(eAnime obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del anime no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarAnime(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar animes *********************************************
        public bool eliminarAnime(int id, out string mensaje)
        {
            return objCapaDatos.eliminarAnime(id, out mensaje);
        }

    }
}
