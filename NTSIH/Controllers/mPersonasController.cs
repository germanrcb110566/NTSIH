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
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var mPersona = db.mPersona.Include(m => m.Catalogo).Include(m => m.Catalogo1).Include(m => m.Catalogo2).Include(m => m.Catalogo3);
            return View(await mPersona.ToListAsync());
        }

        // GET: mPersonas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
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
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
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
            if (ModelState.IsValid)
            {
                try
                {

               
                db.mPersona.Add(mPersona);
                await db.SaveChangesAsync();
                string Mensaje = null;
                string Rol = null;
                string SentenciaSQL = "INSERT INTO RROL_PERSONA VALUES (" + mPersona.registro_id + ",10)";

                try
                {
                    using (var conexion = new SIHEntities())
                    {

                        Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                        if (Rol != null)
                        {
                            Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                        }
                        else
                        {
                            Mensaje = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "0020" + ex.Message;
                }
                string Log = aud.InsertarLog(mPersona.registro_id, 10, 1, SentenciaSQL);

                }
                catch (Exception ex)
                {
                    string Log = aud.InsertarLog(1, 10, 1, ex.Message);
                }

                return RedirectToAction("Index");
            }

            ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return RedirectToAction("Index", "Home");
        }

        // GET: mPersonas/Create
        public ActionResult CreatePaciente()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
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
            if (ModelState.IsValid)
            {
                db.mPersona.Add(mPersona);
                await db.SaveChangesAsync();
                string Mensaje = null;
                string Rol = null;
                string SentenciaSQL = "INSERT INTO RROL_PERSONA VALUES (" + mPersona.registro_id + ",10)";

                try
                {
                    using (var conexion = new SIHEntities())
                    {
                        
                        Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                        if (Rol != null)
                        {
                            Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                        }
                        else
                        {
                            Mensaje = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "0020" + ex.Message;
                }
                string Log = aud.InsertarLog(mPersona.registro_id, 10, 1, SentenciaSQL);



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
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
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
            ViewBag.identificacion_tipo = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.identificacion_tipo);
            ViewBag.genero = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.genero);
            ViewBag.ciudad_residencia = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.ciudad_residencia);
            ViewBag.nacionalidad = new SelectList(db.Catalogo, "registro_id", "nombre", mPersona.nacionalidad);
            return View(mPersona);
        }

        // POST: mPersonas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,identificacion_tipo,identificacion,nombres,apellidos,direccion,telefono,celular,fecha_nacimiento,correo_electronico,genero,ciudad_residencia,nacionalidad,clave,estado")] mPersona mPersona)
        {
            if (ModelState.IsValid)
            {
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
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
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
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
