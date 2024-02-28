using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnVinilo
    {
        private cdVinilo objCapaDatos = new cdVinilo();

        //********************************************* Listar vinilos *********************************************
        public List<eVinilo> listarVinilos(int IDCliente)
        {
            return objCapaDatos.listarVinilos(IDCliente);
        }

        //********************************************* Agregar vinilos *********************************************
        public int insertarVinilo(eVinilo obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.vinilo) || string.IsNullOrWhiteSpace(obj.vinilo))
            {
                mensaje = "El nombre del vinilo no puede estar vacío.";
            }
            else if(string.IsNullOrEmpty(obj.banda) || string.IsNullOrWhiteSpace(obj.banda))
            {
                mensaje = "El nombre de la banda no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.insertarVinilo(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar vinilos *********************************************
        public bool editarVinilo(eVinilo obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.vinilo) || string.IsNullOrWhiteSpace(obj.vinilo))
            {
                mensaje = "El nombre del vinilo no puede estar vacío.";
            }
            if (string.IsNullOrEmpty(obj.banda) || string.IsNullOrWhiteSpace(obj.banda))
            {
                mensaje = "El nombre de la banda no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarVinilo(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Eliminar vinilos *********************************************
        public bool eliminarVinilo(int id, out string mensaje)
        {
            return objCapaDatos.eliminarVinilo(id, out mensaje);
        }

    }
}
