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
    public class mAtencionsController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mAtencions
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var mAtencion = db.mAtencion.Include(m => m.mCita);
            return View(await mAtencion.ToListAsync());
        }

        // GET: mAtencions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAtencion mAtencion = await db.mAtencion.FindAsync(id);
            if (mAtencion == null)
            {
                return HttpNotFound();
            }
            return View(mAtencion);
        }

        // GET: mAtencions/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo");
            return View();
        }

        // POST: mAtencions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cita_id,diagnostico,examenes,receta")] mAtencion mAtencion)
        {
            if (ModelState.IsValid)
            {
                db.mAtencion.Add(mAtencion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mAtencion.cita_id);
            return View(mAtencion);
        }

        // GET: mAtencions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAtencion mAtencion = await db.mAtencion.FindAsync(id);
            if (mAtencion == null)
            {
                return HttpNotFound();
            }
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mAtencion.cita_id);
            return View(mAtencion);
        }

        // POST: mAtencions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cita_id,diagnostico,examenes,receta")] mAtencion mAtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAtencion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mAtencion.cita_id);
            return View(mAtencion);
        }

        // GET: mAtencions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAtencion mAtencion = await db.mAtencion.FindAsync(id);
            if (mAtencion == null)
            {
                return HttpNotFound();
            }
            return View(mAtencion);
        }

        // POST: mAtencions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mAtencion mAtencion = await db.mAtencion.FindAsync(id);
            db.mAtencion.Remove(mAtencion);
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
