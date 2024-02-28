﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace HobbyTrackerCliente.Sesion
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Cliente"] == null) 
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}