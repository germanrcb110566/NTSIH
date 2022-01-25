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
    public class mCalendariosController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mCalendarios
        public async Task<ActionResult> Index()
        {
            return View(await db.mCalendario.ToListAsync());
        }

        // GET: mCalendarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCalendario mCalendario = await db.mCalendario.FindAsync(id);
            if (mCalendario == null)
            {
                return HttpNotFound();
            }
            return View(mCalendario);
        }

        // GET: mCalendarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mCalendarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,nombre,entre_semana,fin_de_semana,horario_desde_m,horario_hasta_m,horario_desde_v,horario_hasta_v,intervalo_citas,intervalo_fechas,estado")] mCalendario mCalendario)
        {
            if (ModelState.IsValid)
            {
                db.mCalendario.Add(mCalendario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mCalendario);
        }

        // GET: mCalendarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCalendario mCalendario = await db.mCalendario.FindAsync(id);
            if (mCalendario == null)
            {
                return HttpNotFound();
            }
            return View(mCalendario);
        }

        // POST: mCalendarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,nombre,entre_semana,fin_de_semana,horario_desde_m,horario_hasta_m,horario_desde_v,horario_hasta_v,intervalo_citas,intervalo_fechas,estado")] mCalendario mCalendario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCalendario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mCalendario);
        }

        // GET: mCalendarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCalendario mCalendario = await db.mCalendario.FindAsync(id);
            if (mCalendario == null)
            {
                return HttpNotFound();
            }
            return View(mCalendario);
        }

        // POST: mCalendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mCalendario mCalendario = await db.mCalendario.FindAsync(id);
            db.mCalendario.Remove(mCalendario);
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
