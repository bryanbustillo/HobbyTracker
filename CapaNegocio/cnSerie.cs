using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnSerie
    {
        private cdSerie objCapaDatos = new cdSerie();

        //********************************************* Listar series *********************************************
        public List<eSerie> listarSeries(int IDCliente)
        {
            return objCapaDatos.listarSeries(IDCliente);
        }

        //********************************************* Agregar series *********************************************
        public int insertarSerie(eSerie obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la serie no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarSerie(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar series *********************************************
        public bool editarSerie(eSerie obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de la serie no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarSerie(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar series *********************************************
        public bool eliminarSerie(int id, out string mensaje)
        {
            return objCapaDatos.eliminarSerie(id, out mensaje);
        }

    }
}
