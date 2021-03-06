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
    public class mAuditoriasController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mAuditorias
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            //var mAuditoria = db.mAuditoria.Include(m => m.mPersona);
            var mAuditoria = db.mAuditoria.OrderByDescending(d => d.fecha).Include(m => m.mPersona);
            return View(await mAuditoria.ToListAsync());
        }

        // GET: mAuditorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAuditoria mAuditoria = await db.mAuditoria.FindAsync(id);
            if (mAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(mAuditoria);
        }

        // GET: mAuditorias/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.usuario_id = new SelectList(db.mPersona, "registro_id", "identificacion");
            return View();
        }

        // POST: mAuditorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,fecha,usuario_id,rol_id,modulo_id,sentencia")] mAuditoria mAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.mAuditoria.Add(mAuditoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.usuario_id = new SelectList(db.mPersona, "registro_id", "identificacion", mAuditoria.usuario_id);
            return View(mAuditoria);
        }

        // GET: mAuditorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAuditoria mAuditoria = await db.mAuditoria.FindAsync(id);
            if (mAuditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.mPersona, "registro_id", "identificacion", mAuditoria.usuario_id);
            return View(mAuditoria);
        }

        // POST: mAuditorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,fecha,usuario_id,rol_id,modulo_id,sentencia")] mAuditoria mAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAuditoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.usuario_id = new SelectList(db.mPersona, "registro_id", "identificacion", mAuditoria.usuario_id);
            return View(mAuditoria);
        }

        // GET: mAuditorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mAuditoria mAuditoria = await db.mAuditoria.FindAsync(id);
            if (mAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(mAuditoria);
        }

        // POST: mAuditorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mAuditoria mAuditoria = await db.mAuditoria.FindAsync(id);
            db.mAuditoria.Remove(mAuditoria);
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
