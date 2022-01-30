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
    public class rMedico_EspecialidadController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: rMedico_Especialidad
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var rMedico_Especialidad = db.rMedico_Especialidad.Include(r => r.Catalogo).Where(x => x.Catalogo.catalogo_id==6).Include(r => r.mPersona);
            return View(await rMedico_Especialidad.ToListAsync());
        }

        // GET: rMedico_Especialidad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rMedico_Especialidad rMedico_Especialidad = await db.rMedico_Especialidad.FindAsync(id);
            if (rMedico_Especialidad == null)
            {
                return HttpNotFound();
            }
            return View(rMedico_Especialidad);
        }

        // GET: rMedico_Especialidad/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            //ViewBag.especialidad_id = new SelectList(db.Catalogo, "registro_id", "nombre");
            //ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion");
            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 6), "registro_id", "nombre");
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres");

            return View();
        }

        // POST: rMedico_Especialidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,medico_id,especialidad_id")] rMedico_Especialidad rMedico_Especialidad)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (ModelState.IsValid)
            {
                db.rMedico_Especialidad.Add(rMedico_Especialidad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(x=>x.catalogo_id==6), "registro_id", "nombre", rMedico_Especialidad.especialidad_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", rMedico_Especialidad.medico_id);

            return View(rMedico_Especialidad);
        }

        // GET: rMedico_Especialidad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rMedico_Especialidad rMedico_Especialidad = await db.rMedico_Especialidad.FindAsync(id);
            if (rMedico_Especialidad == null)
            {
                return HttpNotFound();
            }
            //ViewBag.especialidad_id = new SelectList(db.Catalogo, "registro_id", "nombre", rMedico_Especialidad.especialidad_id);
            //ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion", rMedico_Especialidad.medico_id);

            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 6), "registro_id", "nombre", rMedico_Especialidad.especialidad_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", rMedico_Especialidad.medico_id);

            return View(rMedico_Especialidad);
        }

        // POST: rMedico_Especialidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,medico_id,especialidad_id")] rMedico_Especialidad rMedico_Especialidad)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (ModelState.IsValid)
            {
                db.Entry(rMedico_Especialidad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.especialidad_id = new SelectList(db.Catalogo, "registro_id", "nombre", rMedico_Especialidad.especialidad_id);
            //ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion", rMedico_Especialidad.medico_id);

            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 6), "registro_id", "nombre", rMedico_Especialidad.especialidad_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", rMedico_Especialidad.medico_id);

            return View(rMedico_Especialidad);
        }

        // GET: rMedico_Especialidad/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rMedico_Especialidad rMedico_Especialidad = await db.rMedico_Especialidad.FindAsync(id);
            if (rMedico_Especialidad == null)
            {
                return HttpNotFound();
            }
            return View(rMedico_Especialidad);
        }

        // POST: rMedico_Especialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            rMedico_Especialidad rMedico_Especialidad = await db.rMedico_Especialidad.FindAsync(id);
            db.rMedico_Especialidad.Remove(rMedico_Especialidad);
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
