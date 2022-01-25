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
    public class mCatalogosController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mCatalogos
        public async Task<ActionResult> Index()
        {
            return View(await db.mCatalogo.ToListAsync());
        }

        // GET: mCatalogos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCatalogo mCatalogo = await db.mCatalogo.FindAsync(id);
            if (mCatalogo == null)
            {
                return HttpNotFound();
            }
            return View(mCatalogo);
        }

        // GET: mCatalogos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mCatalogos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,catalogo,estado")] mCatalogo mCatalogo)
        {
            if (ModelState.IsValid)
            {
                db.mCatalogo.Add(mCatalogo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mCatalogo);
        }

        // GET: mCatalogos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCatalogo mCatalogo = await db.mCatalogo.FindAsync(id);
            if (mCatalogo == null)
            {
                return HttpNotFound();
            }
            return View(mCatalogo);
        }

        // POST: mCatalogos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,catalogo,estado")] mCatalogo mCatalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCatalogo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mCatalogo);
        }

        // GET: mCatalogos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCatalogo mCatalogo = await db.mCatalogo.FindAsync(id);
            if (mCatalogo == null)
            {
                return HttpNotFound();
            }
            return View(mCatalogo);
        }

        // POST: mCatalogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mCatalogo mCatalogo = await db.mCatalogo.FindAsync(id);
            db.mCatalogo.Remove(mCatalogo);
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
