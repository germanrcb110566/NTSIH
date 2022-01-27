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
    public class rRol_PersonaController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: rRol_Persona
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var rRol_Persona = db.rRol_Persona.Include(r => r.Catalogo).Include(r => r.mPersona);
            return View(await rRol_Persona.ToListAsync());
        }

        // GET: rRol_Persona/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rRol_Persona rRol_Persona = await db.rRol_Persona.FindAsync(id);
            if (rRol_Persona == null)
            {
                return HttpNotFound();
            }
            return View(rRol_Persona);
        }

        // GET: rRol_Persona/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre");
            ViewBag.persona_id = new SelectList(db.mPersona, "registro_id", "identificacion");
            return View();
        }

        // POST: rRol_Persona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,persona_id,rol_id")] rRol_Persona rRol_Persona)
        {
            if (ModelState.IsValid)
            {
                db.rRol_Persona.Add(rRol_Persona);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", rRol_Persona.rol_id);
            ViewBag.persona_id = new SelectList(db.mPersona, "registro_id", "identificacion", rRol_Persona.persona_id);
            return View(rRol_Persona);
        }

        // GET: rRol_Persona/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rRol_Persona rRol_Persona = await db.rRol_Persona.FindAsync(id);
            if (rRol_Persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", rRol_Persona.rol_id);
            ViewBag.persona_id = new SelectList(db.mPersona, "registro_id", "identificacion", rRol_Persona.persona_id);
            return View(rRol_Persona);
        }

        // POST: rRol_Persona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,persona_id,rol_id")] rRol_Persona rRol_Persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rRol_Persona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", rRol_Persona.rol_id);
            ViewBag.persona_id = new SelectList(db.mPersona, "registro_id", "identificacion", rRol_Persona.persona_id);
            return View(rRol_Persona);
        }

        // GET: rRol_Persona/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rRol_Persona rRol_Persona = await db.rRol_Persona.FindAsync(id);
            if (rRol_Persona == null)
            {
                return HttpNotFound();
            }
            return View(rRol_Persona);
        }

        // POST: rRol_Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rRol_Persona rRol_Persona = await db.rRol_Persona.FindAsync(id);
            db.rRol_Persona.Remove(rRol_Persona);
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
