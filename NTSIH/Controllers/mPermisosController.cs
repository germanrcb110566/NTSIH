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
    public class mPermisosController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mPermisos
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            var mPermisos = db.mPermisos.Include(m => m.Catalogo).Include(m => m.Catalogo1).Include(m => m.Catalogo2);
            return View(await mPermisos.ToListAsync());
        }

        // GET: mPermisos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPermisos mPermisos = await db.mPermisos.FindAsync(id);
            if (mPermisos == null)
            {
                return HttpNotFound();
            }
            return View(mPermisos);
        }

        // GET: mPermisos/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre");
            ViewBag.modulo_id = new SelectList(db.Catalogo, "registro_id", "nombre");
            ViewBag.accion_id = new SelectList(db.Catalogo, "registro_id", "nombre");
            return View();
        }

        // POST: mPermisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,rol_id,modulo_id,accion_id,estado")] mPermisos mPermisos)
        {
            if (ModelState.IsValid)
            {
                db.mPermisos.Add(mPermisos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.rol_id);
            ViewBag.modulo_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.modulo_id);
            ViewBag.accion_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.accion_id);
            return View(mPermisos);
        }

        // GET: mPermisos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPermisos mPermisos = await db.mPermisos.FindAsync(id);
            if (mPermisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.rol_id);
            ViewBag.modulo_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.modulo_id);
            ViewBag.accion_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.accion_id);
            return View(mPermisos);
        }

        // POST: mPermisos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,rol_id,modulo_id,accion_id,estado")] mPermisos mPermisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mPermisos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.rol_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.rol_id);
            ViewBag.modulo_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.modulo_id);
            ViewBag.accion_id = new SelectList(db.Catalogo, "registro_id", "nombre", mPermisos.accion_id);
            return View(mPermisos);
        }

        // GET: mPermisos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.Acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mPermisos mPermisos = await db.mPermisos.FindAsync(id);
            if (mPermisos == null)
            {
                return HttpNotFound();
            }
            return View(mPermisos);
        }

        // POST: mPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mPermisos mPermisos = await db.mPermisos.FindAsync(id);
            db.mPermisos.Remove(mPermisos);
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
