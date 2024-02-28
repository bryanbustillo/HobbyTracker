using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class cnCliente
    {
        private cdCliente objCapaDatos = new cdCliente();

        //********************************************* Listar clientes *********************************************
        public List<eCliente> listarClientes()
        {
            return objCapaDatos.ListarClientes();
        }

        //********************************************* Listar clientes *********************************************
        public List<eCliente> ListarClientesPerfil(int IDCliente)
        {
            return objCapaDatos.ListarClientesPerfil(IDCliente);
        }

        //********************************************* Agregar clientes *********************************************
        public int insertarClientes(eCliente obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del cliente no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "El apellido del cliente no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "El email del cliente no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                //no estoy encriptando la contraseña porque no está funcionando en el método Login del controlador Acceso
                //obj.contrasena = cnRecursos.encriptarContrasena(obj.contrasena);
                return objCapaDatos.insertarCliente(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //********************************************* Editar usuarios *********************************************
        public bool editarCliente(eCliente obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del cliente no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "El apellido del cliente no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "El email del cliente no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDatos.editarCliente(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        //********************************************* Cambiar contraseña peril *********************************************
        public bool cambiarContrasenaPerfil(int IDCliente, string nuevaContrasena, out string mensaje)
        {
            return objCapaDatos.cambiarContrasenaPerfil(IDCliente, nuevaContrasena, out mensaje);
        }

        //********************************************* Cambiar contraseña *********************************************
        public bool cambiarContrasena(int IDCliente, string nuevaContrasena, out string mensaje)
        {
            return objCapaDatos.cambiarContrasena(IDCliente, nuevaContrasena, out mensaje);
        }

        //********************************************* Restablecer contraseña *********************************************
        public bool restablecerContrasena(int IDCliente, string email, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaContrasena = cnRecursos.generarContrasena();
            //no estoy encriptando la contraseña porque no está funcionando en el método Login del controlador Acceso
            //bool resultado = objCapaDatos.restablecerContrasena(IDCliente, cnRecursos.encriptarContrasena(nuevaContrasena), out Mensaje);
            bool resultado = objCapaDatos.restablecerContrasena(IDCliente, nuevaContrasena, out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña restablecida";
                string mensaje_email = "<h3>Su contraseña fue restablecida correctamente</h3></br><p>Su contraseña ahora es: !nuevaContrasena!</p>";
                mensaje_email = mensaje_email.Replace("!nuevaContrasena!", nuevaContrasena);

                bool respuesta = cnRecursos.enviarContrasenaCorreo(email, asunto, mensaje_email);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo restablecer la contraseña";

                return false;
            }
        }

    }
}
