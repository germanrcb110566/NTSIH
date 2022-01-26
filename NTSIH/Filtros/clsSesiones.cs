using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSIH.Controllers;
using NTSIH.Models;

namespace NTSIH.Filtros
{
    public class clsSesiones : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request.RawUrl.ToString() == "/mPersonas/CreatePaciente")
            {
                // filterContext.HttpContext.Response.Redirect("~/Registro/Registrar");
            }
            else
            {

                var oUser = (mPersona)HttpContext.Current.Session["identificacion"];

                if (oUser == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Index");
                    }
                }
                else
                {
                    if (filterContext.Controller is AccesoController == true)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Index");
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}