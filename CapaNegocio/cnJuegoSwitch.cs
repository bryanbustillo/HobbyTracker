using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnJuegoSwitch
    {
        private cdJuegoSwitch objCapaDatos = new cdJuegoSwitch();

        //********************************************* Listar juegos switch *********************************************
        public List<eJuegoSwitch> listarJuegosSwitch(int IDCliente)
        {
            return objCapaDatos.listarJuegosSwitch(IDCliente);
        }

        //********************************************* Agregar juegos switch *********************************************
        public int insertarJuegoSwitch(eJuegoSwitch obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.juego) || string.IsNullOrWhiteSpace(obj.juego))
            {
                mensaje = "El nombre del juego no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarJuegoSwitch(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar juegos switch *********************************************
        public bool editarJuegoSwitch(eJuegoSwitch obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.juego) || string.IsNullOrWhiteSpace(obj.juego))
            {
                mensaje = "El nombre del juego no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarJuegoSwitch(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar juegos switch *********************************************
        public bool eliminarJuegoSwitch(int id, out string mensaje)
        {
            return objCapaDatos.eliminarJuegoSwitch(id, out mensaje);
        }

    }
}
