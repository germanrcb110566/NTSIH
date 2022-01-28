using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSIH.Models;

namespace NTSIH.Controllers
{
    public class AccesoController : Controller
    {
        private mPersona per = new mPersona();
        private rRol_Persona rtper = new rRol_Persona();
        private mCatalogo mcat = new mCatalogo();
        private Catalogo cat = new Catalogo();
        private mAuditoria aud = new mAuditoria();

        public ActionResult Index()
        {
            ViewBag.alerta = "info";
            ViewBag.mensaje = "Acceso al Sistema".ToUpper();
            ViewBag.layout = "~/Views/Shared/_Layout.cshtml";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string identificacion, string password)
        {
            try
            {
                string Mensaje = "";
                mPersona oUser = per.unRegistro(ref Mensaje, identificacion);
                if (Mensaje.Substring(0, 4) != "0000")
                {

                    ViewBag.alerta = "danger";
                    ViewBag.res = Mensaje.Substring(4).ToUpper();
                    return View();
                }
                int Persona_Id = oUser.registro_id;         //Se obtiene el numero de Id de la Persona que esta accediendo
                string oRol = cat.ObtenerRol(ref Mensaje, identificacion, Persona_Id );
                if (Mensaje.Substring(0, 4) != "0000")
                {
                    ViewBag.alerta = "danger";
                    ViewBag.res = "Error: Persona no Tiene Asignado un ROL en e Sistema".ToUpper();
                    return View();
                }
                password = EncDecryptController.GetSHA256(password);
                if (oUser.clave == password)
                {
                    Session["Id_mPersona"] = oUser.registro_id;
                    Session["identificacion"] = oUser;
                    Session["Nombres"] = oUser.nombres;
                    Session["Rol"] = oRol;
                    ViewBag.alerta = "success";
                    ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
                    Session["Layout"] = ViewBag.layout;
                    ViewBag.Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
                    if (oRol == "PACIENTE")
                    {
                        ViewBag.layout = "~/Views/Shared/_LayoutPaciente.cshtml";                     
                    }
                    if (oRol == "ADMINISTRADOR")
                    {
                        ViewBag.layout = "~/Views/Shared/_LayoutAdmin.cshtml";                
                    }
                    if (oRol == "AUXILIAR" && oRol == "SECRETARIA" && oRol == "SERVICIOS GENERALES")
                    {
                        ViewBag.layout = "~/Views/Shared/_LayoutAsistente.cshtml";                  
                    }
                    if (oRol == "MÉDICO")
                    {
                        ViewBag.layout = "~/Views/Shared/_LayoutMedico.cshtml";                        
                    }
                    Session["Layout"] = ViewBag.layout;
                    return Content(oRol);
                }
                else
                {
                    ViewBag.alerta = "danger";
                    ViewBag.res = "Error: Clave Ingresada NO es correcta".ToUpper();
                }
            }
            catch (Exception ex)
            {
                ViewBag.alerta = "danger";
                ViewBag.res = "Error: ".ToUpper() + ex.Message.ToUpper();
            }
            return View();
        }


        public ActionResult Cerrar()
        {
            Session["identificacion"] = null;
            Session["Rol"] = null;
            Session["Nombres"] = null;
            return View();
        }
    }
}