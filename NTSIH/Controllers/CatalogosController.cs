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
    public class CatalogosController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: Catalogos
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "info";
            ViewBag.mensaje = "Index:Maestro de Catalogos".ToUpper();

            var catalogo = db.Catalogo.Include(c => c.mCatalogo);
            return View(await catalogo.ToListAsync());
        }

        // GET: Catalogos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogo.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // GET: Catalogos/Create
        public ActionResult Create()
        {
            ViewBag.catalogo_id = new SelectList(db.mCatalogo, "registro_id", "catalogo");
            return View();
        }

        // POST: Catalogos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,catalogo_id,nombre,descripcion,estado")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Catalogo.Add(catalogo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.catalogo_id = new SelectList(db.mCatalogo, "registro_id", "catalogo", catalogo.catalogo_id);
            return View(catalogo);
        }

        // GET: Catalogos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogo.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.catalogo_id = new SelectList(db.mCatalogo, "registro_id", "catalogo", catalogo.catalogo_id);
            return View(catalogo);
        }

        // POST: Catalogos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,catalogo_id,nombre,descripcion,estado")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.catalogo_id = new SelectList(db.mCatalogo, "registro_id", "catalogo", catalogo.catalogo_id);
            return View(catalogo);
        }

        // GET: Catalogos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogo.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Catalogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Catalogo catalogo = await db.Catalogo.FindAsync(id);
            db.Catalogo.Remove(catalogo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
