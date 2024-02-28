using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using CapaEntidades;
using CapaNegocio;

namespace HobbyTrackerCliente.Controllers
{
    public class AccesoController : Controller
    {
        #region VISTAS
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegistrarCliente()
        {
            return View();
        }

        public ActionResult RestablecerContrasena()
        {
            return View();
        }

        public ActionResult CambiarContrasena()
        {
            return View();
        }
        #endregion

        #region MÉTODOS
        //********************************************* Registrar cliente *********************************************
        [HttpPost]
        public ActionResult RegistrarCliente(eCliente obj)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["nombre"] = string.IsNullOrEmpty(obj.nombre) ? "" : obj.nombre;
            ViewData["apellido"] = string.IsNullOrEmpty(obj.apellido) ? "" : obj.apellido;
            ViewData["email"] = string.IsNullOrEmpty(obj.email) ? "" : obj.email;

            if (obj.contrasena != obj.confirmarContrasena)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            resultado = new cnCliente().insertarClientes(obj, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        //********************************************* Loguear cliente *********************************************
        [HttpPost]
        public ActionResult Login(string email, string contrasena)
        {
            eCliente objCliente = null;
            //devuelve null cuando se guarda la contraseña encriptada
            //objCliente = new cnCliente().listarClientes().Where(item => item.email == email && item.contrasena == cnRecursos.encriptarContrasena(contrasena)).FirstOrDefault();
            objCliente = new cnCliente().listarClientes().FirstOrDefault(item => item.email == email && item.contrasena == contrasena);


            if (objCliente == null)
            {
                ViewBag.Error = "El email o contraseña no válidos";
                return View();
            }
            else
            {                
                if (objCliente.restablecerContrasena)
                {
                    //Guarda temporalmente el IDCliente
                    TempData["IDCliente"] = objCliente.IDCliente;
                    return RedirectToAction("CambiarContrasena", "Acceso");
                }
                else
                {
                    //Autenticar el cliente
                    //FormsAuthentication.SetAuthCookie(objCliente.email, false);
                    FormsAuthentication.SetAuthCookie(objCliente.email + "|" + objCliente.nombre + "|" + objCliente.apellido + "|" + objCliente.IDCliente, false);

                    //Guarda la info en una sesión
                    Session["Cliente"] = objCliente;
                    ViewBag.Error = null;

                    return RedirectToAction("Index","Home");
                }
            }
        }

        //********************************************* Restablecer contraseña *********************************************
        [HttpPost]
        public ActionResult RestablecerContrasena(string email)
        {
            eCliente objCliente = new eCliente();

            objCliente = new cnCliente().listarClientes().Where(item => item.email == email).FirstOrDefault();

            if (objCliente == null)
            {
                ViewBag.Error = "No se encontró ningún cliente relacionado a este correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new cnCliente().restablecerContrasena(objCliente.IDCliente, email, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Login", "Acceso");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        //********************************************* Cambiar contraseña *********************************************
        [HttpPost]
        public ActionResult CambiarContrasena(string IDCliente, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            eCliente objCliente = new eCliente();
            objCliente = new cnCliente().listarClientes().Where(item => item.IDCliente == int.Parse(IDCliente)).FirstOrDefault();

            //if (objCliente.contrasena != cnRecursos.encriptarContrasena(contrasenaActual))
            if (objCliente.contrasena != contrasenaActual)
            {
                TempData["IDCliente"] = IDCliente;
                ViewData["vContrasena"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaContrasena != confirmarContrasena)
            {

                TempData["IDCliente"] = IDCliente;
                ViewData["vContrasena"] = contrasenaActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vContrasena"] = "";

            //no estoy encriptando la contraseña porque no está funcionando en el método Login del controlador Acceso
            //nuevaContrasena = cnRecursos.encriptarContrasena(nuevaContrasena);

            string mensaje = string.Empty;
            bool respuesta = new cnCliente().cambiarContrasena(int.Parse(IDCliente), nuevaContrasena, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Login");
            }
            else
            {
                TempData["IDCliente"] = IDCliente;
                ViewBag.Error = mensaje;

                return View();
            }
        }

        //********************************************* Cerrar sesión *********************************************
        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Acceso");
        }
        #endregion

    }
}