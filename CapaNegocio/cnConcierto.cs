using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnConcierto
    {
        private cdConcierto objCapaDatos = new cdConcierto();

        //********************************************* Listar conciertos *********************************************
        public List<eConcierto> listarConcierto(int IDCliente)
        {
            return objCapaDatos.listarConciertos(IDCliente);
        }

        //********************************************* Agregar conciertos *********************************************
        public int insertarConcierto(eConcierto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.concierto) || string.IsNullOrWhiteSpace(obj.concierto))
            {
                mensaje = "El nombre del concierto no puede estar vacío.";
            }
            else if(string.IsNullOrEmpty(obj.lugar) || string.IsNullOrWhiteSpace(obj.lugar))
            {
                mensaje = "El nombre del lugar no puede estar vacío.";
            }
            //if (obj.fecha == DateTime.MinValue)
            else if(string.IsNullOrEmpty(obj.fecha) || string.IsNullOrWhiteSpace(obj.fecha))
            {
                mensaje = "La fecha no puede estar vacía.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarConcierto(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar conciertos *********************************************
        public bool editarConcierto(eConcierto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.concierto) || string.IsNullOrWhiteSpace(obj.concierto))
            {
                mensaje = "El nombre del concierto no puede estar vacío.";
            }
            if (string.IsNullOrEmpty(obj.lugar) || string.IsNullOrWhiteSpace(obj.lugar))
            {
                mensaje = "El nombre del lugar no puede estar vacío.";
            }
            //if (obj.fecha == DateTime.MinValue)
            if (string.IsNullOrEmpty(obj.fecha) || string.IsNullOrWhiteSpace(obj.fecha))
            {
                mensaje = "La fecha no puede estar vacía.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarConcierto(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar conciertos *********************************************
        public bool eliminarConcierto(int id, out string mensaje)
        {
            return objCapaDatos.eliminarConcierto(id, out mensaje);
        }

    }
}
