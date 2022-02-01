﻿using System;
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
    public class mExamenesController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: mExamenes
        public async Task<ActionResult> Index()
        {
            var mTratamiento = db.mTratamiento.Include(m => m.Catalogo).Where(x => x.Catalogo.catalogo_id == 11).Include(m => m.mCita);
            return View(await mTratamiento.ToListAsync());
        }

        // GET: mExamenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mTratamiento mTratamiento = await db.mTratamiento.FindAsync(id);
            if (mTratamiento == null)
            {
                return HttpNotFound();
            }
            return View(mTratamiento);
        }

        // GET: mExamenes/Create
        public ActionResult Create()
        {
            ViewBag.catalogo_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 11), "registro_id", "nombre");
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo");
            return View();
        }

        // POST: mExamenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,cita_id,catalogo_id,cantidad,prescripcion")] mTratamiento mTratamiento)
        {
            if (ModelState.IsValid)
            {
                db.mTratamiento.Add(mTratamiento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.catalogo_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 11), "registro_id", "nombre", mTratamiento.catalogo_id);
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mTratamiento.cita_id);
            return View(mTratamiento);
        }

        // GET: mExamenes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mTratamiento mTratamiento = await db.mTratamiento.FindAsync(id);
            if (mTratamiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.catalogo_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 11), "registro_id", "nombre", mTratamiento.catalogo_id);
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mTratamiento.cita_id);
            return View(mTratamiento);
        }

        // POST: mExamenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,cita_id,catalogo_id,cantidad,prescripcion")] mTratamiento mTratamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mTratamiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.catalogo_id = new SelectList(db.Catalogo.Where(x => x.catalogo_id == 11), "registro_id", "nombre", mTratamiento.catalogo_id);
            ViewBag.cita_id = new SelectList(db.mCita, "registro_id", "motivo", mTratamiento.cita_id);
            return View(mTratamiento);
        }

        // GET: mExamenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mTratamiento mTratamiento = await db.mTratamiento.FindAsync(id);
            if (mTratamiento == null)
            {
                return HttpNotFound();
            }
            return View(mTratamiento);
        }

        // POST: mExamenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mTratamiento mTratamiento = await db.mTratamiento.FindAsync(id);
            db.mTratamiento.Remove(mTratamiento);
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
