using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidades;
using CapaNegocio;
using Newtonsoft.Json;

using System.Web.Security;
using HobbyTrackerCliente.Sesion; // carpeta Sesion

namespace HobbyTrackerCliente.Controllers
{
    public class HomeController : Controller
    {
        #region VISTAS        
        [ValidarSesion] // evita ir a la vista si no ha iniciado sesión (carpeta Sesion ahí esta el método)
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Clientes()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Libros()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Peliculas()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult CategoriaPeliculas()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Series()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Vinilos()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Conciertos()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult JuegoSwitch()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult Animes()
        {
            return View();
        }

        [ValidarSesion]
        [Authorize]
        public ActionResult AnimePeliculas()
        {
            return View();
        }
        #endregion

        #region CLIENTES
        //********************************************* Listar clientes *********************************************
        [HttpGet]
        public JsonResult listarClientes()
        {
            List<eCliente> listaClientes = new List<eCliente>();

            listaClientes = new cnCliente().listarClientes();

            return Json(new { data = listaClientes }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Listar clientes perfil *********************************************
        [HttpGet]
        public JsonResult ListarClientesPerfil(int IDCliente)
        {
            List<eCliente> listaClientes = new List<eCliente>();

            listaClientes = new cnCliente().ListarClientesPerfil(IDCliente);

            return Json(new { data = listaClientes }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Editar clientes ****************************************
        [HttpPost]
        public JsonResult editarUsuario(eCliente obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDCliente != 0)
            {
                resultado = new cnCliente().editarCliente(obj, out mensaje);
            }
            else
            {
                try
                {
                    // Código que puede generar un error
                    throw new Exception("Error: El ID del cliente es 0.");
                }
                catch (Exception ex)
                {
                    // Capturar el error y asignarlo al mensaje
                    mensaje = ex.Message;
                    resultado = null; // Dependiendo de tu lógica, podrías asignar null al resultado
                }
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Cambiar contraseña perfil ****************************************
        [HttpPost]
        public ActionResult cambiarContrasenaPerfil(string IDCliente, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            eCliente objCliente = new eCliente();
            objCliente = new cnCliente().listarClientes().Where(item => item.IDCliente == int.Parse(IDCliente)).FirstOrDefault();

            //if (objCliente.contrasena != cnRecursos.encriptarContrasena(contrasenaActual))
            if (objCliente.contrasena != contrasenaActual)
            {
                //TempData["IDCliente"] = IDCliente;
                //ViewData["vContrasena"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return Json(new { resultado = false, mensaje = "La contraseña actual no es correcta" });
            }
            else if (nuevaContrasena != confirmarContrasena)
            {
                //TempData["IDCliente"] = IDCliente;
                //ViewData["vContrasena"] = contrasenaActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return Json(new { resultado = false, mensaje = "Las contraseñas no coinciden" });
            }

            //ViewData["vContrasena"] = "";

            //no estoy encriptando la contraseña porque no está funcionando en el método Login del controlador Acceso
            //nuevaContrasena = cnRecursos.encriptarContrasena(nuevaContrasena);

            string mensaje = string.Empty;
            bool respuesta = new cnCliente().cambiarContrasenaPerfil(int.Parse(IDCliente), nuevaContrasena, out mensaje);

            if (respuesta)
            {
                return Json(new { resultado = true, mensaje = "La contraseña se cambió correctamente" });
            }
            else
            {
                //TempData["IDCliente"] = IDCliente;
                ViewBag.Error = mensaje;
                return Json(new { resultado = false, mensaje = mensaje });
            }
        }
        #endregion

        #region LIBROS
        //********************************************* Listar libros *********************************************
        [HttpGet]
        public JsonResult listarLibros(int IDCliente)
        {
            List<eLibro> listaLibros = new List<eLibro>();

            listaLibros = new cnLibro().listarLibros(IDCliente);

            return Json(new { data = listaLibros }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar libros ****************************************
        [HttpPost]
        public JsonResult insertarEditarLibro(eLibro obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDLibro == 0)
            {
                resultado = new cnLibro().insertarLibro(obj, out mensaje);
            }
            else
            {
                resultado = new cnLibro().editarLibro(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar libros *********************************************
        [HttpPost]
        public JsonResult eliminarLibro(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnLibro().eliminarLibro(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PELÍCULAS
        //********************************************* Listar películas *********************************************
        [HttpGet]
        public JsonResult listarPeliculas(int IDCliente)
        {
            List<ePelicula> listaPeliculas = new List<ePelicula>();

            listaPeliculas = new cnPelicula().listarPeliculas(IDCliente);

            return Json(new { data = listaPeliculas }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar películas ****************************************
        [HttpPost]
        public JsonResult insertarPelicula(string obj)
        {
            object resultado;
            string mensaje = string.Empty;

            ePelicula oPelicula = new ePelicula();
            oPelicula = JsonConvert.DeserializeObject<ePelicula>(obj);

            if (oPelicula.IDPelicula == 0)
            {
                resultado = new cnPelicula().insertarPelicula(oPelicula, out mensaje);
            }
            else
            {
                resultado = new cnPelicula().editarPelicula(oPelicula, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar películas *********************************************
        [HttpPost]
        public JsonResult eliminarPelicula(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnPelicula().eliminarPelicula(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CATEGORÍAS PELÍCULAS
        //********************************************* Listar categorías *********************************************
        [HttpGet]
        public JsonResult listarCategorias(int IDCliente)
        {
            List<eCategoriaPelicula> listaCategorias = new List<eCategoriaPelicula>();

            listaCategorias = new cnCategoriaPelicula().listarCategorias(IDCliente);

            return Json(new { data = listaCategorias }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar categorías ****************************************
        [HttpPost]
        public JsonResult insertarCategoria(eCategoriaPelicula obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDCategoria == 0)
            {
                resultado = new cnCategoriaPelicula().insertarCategoria(obj, out mensaje);
            }
            else
            {
                resultado = new cnCategoriaPelicula().editarCategoria(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar categorías *********************************************
        [HttpPost]
        public JsonResult eliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnCategoriaPelicula().eliminarCategoria(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SERIES
        //********************************************* Listar series *********************************************
        [HttpGet]
        public JsonResult listarSeries(int IDCliente)
        {
            List<eSerie> listaSeries = new List<eSerie>();

            listaSeries = new cnSerie().listarSeries(IDCliente);

            return Json(new { data = listaSeries }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar series ****************************************
        [HttpPost]
        public JsonResult insertarSerie(eSerie obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDSerie == 0)
            {
                resultado = new cnSerie().insertarSerie(obj, out mensaje);
            }
            else
            {
                resultado = new cnSerie().editarSerie(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar series *********************************************
        [HttpPost]
        public JsonResult eliminarSerie(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnSerie().eliminarSerie(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region VINILOS
        //********************************************* Listar vinilos *********************************************
        [HttpGet]
        public JsonResult listarVinilos(int IDCliente)
        {
            List<eVinilo> listaVinilos = new List<eVinilo>();

            listaVinilos = new cnVinilo().listarVinilos(IDCliente);

            return Json(new { data = listaVinilos }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar vinilos ****************************************
        [HttpPost]
        public JsonResult insertarVinilo(eVinilo obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDVinilo == 0)
            {
                resultado = new cnVinilo().insertarVinilo(obj, out mensaje);
            }
            else
            {
                resultado = new cnVinilo().editarVinilo(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar vinilos *********************************************
        [HttpPost]
        public JsonResult eliminarVinilo(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnVinilo().eliminarVinilo(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CONCIERTOS
        //********************************************* Listar conciertos *********************************************
        [HttpGet]
        public JsonResult listarConciertos(int IDCliente)
        {
            List<eConcierto> listaConciertos = new List<eConcierto>();

            listaConciertos = new cnConcierto().listarConcierto(IDCliente);

            return Json(new { data = listaConciertos }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar conciertos ****************************************
        [HttpPost]
        public JsonResult insertarConcierto(eConcierto obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDConcierto == 0)
            {
                resultado = new cnConcierto().insertarConcierto(obj, out mensaje);
            }
            else
            {
                resultado = new cnConcierto().editarConcierto(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar conciertos *********************************************
        [HttpPost]
        public JsonResult eliminarConcierto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnConcierto().eliminarConcierto(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JUEGOS SWITCH
        //********************************************* Listar juegos switch *********************************************
        [HttpGet]
        public JsonResult listarJuegosSwitch(int IDCliente)
        {
            List<eJuegoSwitch> listaJuegosSwitch = new List<eJuegoSwitch>();

            listaJuegosSwitch = new cnJuegoSwitch().listarJuegosSwitch(IDCliente);

            return Json(new { data = listaJuegosSwitch }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar juegos switch ****************************************
        [HttpPost]
        public JsonResult insertarJuegoSwitch(eJuegoSwitch obj)
        {
            object resultado;
            string mensaje = string.Empty;                       

            if (obj.IDJuegoSwitch == 0)
            {
                resultado = new cnJuegoSwitch().insertarJuegoSwitch(obj, out mensaje);
            }
            else
            {
                resultado = new cnJuegoSwitch().editarJuegoSwitch(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar juegos switch *********************************************
        [HttpPost]
        public JsonResult eliminarJuegoSwitch(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnJuegoSwitch().eliminarJuegoSwitch(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ANIMES
        //********************************************* Listar animes *********************************************
        [HttpGet]
        public JsonResult listarAnimes(int IDCliente)
        {
            List<eAnime> listaAnimes = new List<eAnime>();

            listaAnimes = new cnAnime().listarAnimes(IDCliente);

            return Json(new { data = listaAnimes }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar animes ****************************************
        [HttpPost]
        public JsonResult insertarAnime(eAnime obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDAnime == 0)
            {
                resultado = new cnAnime().insertarAnime(obj, out mensaje);
            }
            else
            {
                resultado = new cnAnime().editarAnime(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar animes *********************************************
        [HttpPost]
        public JsonResult eliminarAnime(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnAnime().eliminarAnime(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ANIME PELÍCULAS
        //********************************************* Listar animes películas *********************************************
        [HttpGet]
        public JsonResult listarAnimePeliculas(int IDCliente)
        {
            List<eAnimePelicula> listaAnimePeliculas = new List<eAnimePelicula>();

            listaAnimePeliculas = new cnAnimePelicula().listarAnimePeliculas(IDCliente);

            return Json(new { data = listaAnimePeliculas }, JsonRequestBehavior.AllowGet);
        }

        //**************************************** Agregar y editar animes películas ****************************************
        [HttpPost]
        public JsonResult insertarAnimePelicula(eAnimePelicula obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IDAnimePelicula == 0)
            {
                resultado = new cnAnimePelicula().insertarAnimePelicula(obj, out mensaje);
            }
            else
            {
                resultado = new cnAnimePelicula().editarAnimePelicula(obj, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //********************************************* Eliminar animes películas *********************************************
        [HttpPost]
        public JsonResult eliminarAnimePelicula(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new cnAnimePelicula().eliminarAnimePelicula(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}