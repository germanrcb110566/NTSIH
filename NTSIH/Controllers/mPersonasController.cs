using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NTSIH.Models;

namespace NTSIH.Controllers 
{
    
    public class mPersonasController : Controller
    {
        private SIHEntities db = new SIHEntities();
        private mAuditoria aud = new mAuditoria();

        // GET: mPersonas
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var mPersona = db.mPersona.Include(m => m.Catalogo).Include(m => m.Catalogo1).Include(m => m.Catalogo2).Include(m => m.Catalogo3);
            return View(await mPersona.ToListAsync());
        }

        // GET: mPersonas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.msgmodulo = Session["mensaje"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPersona mPersona = await db.mPersona.FindAsync(id);
            if (mPersona == null)
            {
                return HttpNotFound();
            }
            return View(mPersona);
        }

        // GET: mPersonas/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.identificacion_tipo = new SelectList(db.Catalogo.Where(d => d.catalogo_id==1), "registro_id", "nombre");
            ViewBag.genero = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 2), "registro_id", "nombre");
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 8), "registro_id", "nombre");
            ViewBag.nacionalidad = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 9), "registro_id", "nombre");
            return View();
        }

        // POST: mPersonas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,identificacion_tipo,identificacion,nombres,apellidos,direccion,telefono,celular,fecha_nacimiento,correo_electronico,genero,ciudad_residencia,nacionalidad,clave,estado")] mPersona mPersona)
        {
            string Rol = null;
            string SentenciaSQL = null;
            if (ModelState.IsValid)
            {
                try
                {
                    mPersona.clave = EncDecryptController.GetSHA256(mPersona.clave);
                    mPersona.estado = true;
                    mPersona.nombres = mPersona.nombres.ToUpper();
                    mPersona.apellidos = mPersona.apellidos.ToUpper();
                    mPersona.direccion = mPersona.direccion.ToUpper();
                    db.mPersona.Add(mPersona);
                    await db.SaveChangesAsync();                
                    Rol = null;
                    SentenciaSQL = "INSERT INTO RROL_PERSONA VALUES (" + mPersona.registro_id + ",10)";

                    try
                    {
                        using (var conexion = new SIHEntities())
                        {

                            Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                            if (Rol != null)
                            {
                                Session["error"] = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                            }
                            else
                            {
                                Session["error"] = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Session["error"] = "0020" + ex.Message;
                    }
                    string Log = EncDecryptController.InsertarLog(mPersona.registro_id, 10, 1, SentenciaSQL);

                }
                catch (Exception ex)
                {
                    string Log = EncDecryptController.InsertarLog(1, 10, 1, ex.Message);
                }
                Session["error"] = "0001:Error en la estructrura de los datos".ToUpper();
                return RedirectToAction("Index");
            }

            ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return RedirectToAction("Index");
        }

        // GET: mPersonas/Create
        public ActionResult CreatePaciente()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.identificacion_tipo = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 1), "registro_id", "nombre");
            ViewBag.genero = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 2), "registro_id", "nombre");
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 8), "registro_id", "nombre");
            ViewBag.nacionalidad = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 9), "registro_id", "nombre");
            return View();
        }

        // POST: mPersonas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePaciente([Bind(Include = "registro_id,identificacion_tipo,identificacion,nombres,apellidos,direccion,telefono,celular,fecha_nacimiento,correo_electronico,genero,ciudad_residencia,nacionalidad,clave,estado")] mPersona mPersona)
        {
            string SentenciaSQL = null;         
            string Rol = null;
            if (ModelState.IsValid)
            {
                try
                {
                    mPersona.clave = EncDecryptController.GetSHA256(mPersona.clave);
                    db.mPersona.Add(mPersona);
                    await db.SaveChangesAsync();
                    
                    SentenciaSQL = "INSERT INTO RROL_PERSONA VALUES (" + mPersona.registro_id + ",10)";

                    try
                    {
                        using (var conexion = new SIHEntities())
                        {

                            Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                            if (Rol != null)
                            {
                                Session["mensaje"] = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                            }
                            else
                            {
                                Session["mensaje"] = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Session["mensaje"] = "0020:ERROR AL GRABAR EN EL MÓDULO ROL_PERSONAS.  INFORMACION ADICIONAL:" + ex.Message;                     
                    }
                }
                catch (Exception ex)
                {
                    Session["mensaje"] = "0020:ERROR AL GRABAR EN EL MÓDULO PERSONAS.  INFORMACION ADICIONAL:" + ex.Message;                 
                    //throw new MiExcepciones(Mensaje);
                }
                
                string Log = EncDecryptController.InsertarLog(mPersona.registro_id, 10, 1, SentenciaSQL);

                return RedirectToAction("Index");
            }

            ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return View();
        }
        // GET: mPersonas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPersona mPersona = await db.mPersona.FindAsync(id);
            if (mPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.identificacion_tipo = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 1), "registro_id", "nombre");
            ViewBag.genero = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 2), "registro_id", "nombre");
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 8), "registro_id", "nombre");
            ViewBag.nacionalidad = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 9), "registro_id", "nombre");
            //ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            //ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            //ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            //ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return View(mPersona);
        }

        // POST: mPersonas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,identificacion_tipo,identificacion,nombres,apellidos,direccion,telefono,celular,fecha_nacimiento,correo_electronico,genero,ciudad_residencia,nacionalidad,clave,estado")] mPersona mPersona)
        {
            if (mPersona.clave != null)
            {
                string clave = EncDecryptController.GetSHA256(mPersona.clave);
                mPersona.clave = clave;
            }
            
            if (ModelState.IsValid)
            {
                //mPersona.clave = EncDecryptController.GetSHA256(mPersona.clave);
                db.Entry(mPersona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return View(mPersona);
        }

        // GET: mPersonas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPersona mPersona = await db.mPersona.FindAsync(id);
            if (mPersona == null)
            {
                return HttpNotFound();
            }
            return View(mPersona);
        }

        // POST: mPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mPersona mPersona = await db.mPersona.FindAsync(id);
            db.mPersona.Remove(mPersona);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
