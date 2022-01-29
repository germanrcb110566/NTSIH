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
    [HandleError(View = "Error")]
    public class rMedico_CalendarioController : Controller
    {
        private SIHEntities db = new SIHEntities();

        // GET: rMedico_Calendario
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            //var rMedico_Calendario = db.rMedico_Calendario.Include(r => r.mCalendario).Include(r => r.mPersona);
            var rMedico_Calendario = db.rMedico_Calendario.Include(r => r.mPersona).Include(r => r.mCalendario);
            return View(await rMedico_Calendario.ToListAsync());
        }

        // GET: rMedico_Calendario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rMedico_Calendario rMedico_Calendario = await db.rMedico_Calendario.FindAsync(id);
            if (rMedico_Calendario == null)
            {
                return HttpNotFound();
            }
            return View(rMedico_Calendario);
        }

        // GET: rMedico_Calendario/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            ViewBag.calendario_id = new SelectList(db.mCalendario, "registro_id", "nombre");
            ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion");
            return View();
        }

        // POST: rMedico_Calendario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,medico_id,calendario_id")] rMedico_Calendario rMedico_Calendario)
        {
            if (ModelState.IsValid)
            {
                db.rMedico_Calendario.Add(rMedico_Calendario);
                try
                {
                    await db.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    throw new MiExcepciones("Error al Actualizar la Información:".ToUpper() + ex.Message.ToUpper());
                }
                
               // return RedirectToAction("Index");
            }

            ViewBag.calendario_id = new SelectList(db.mCalendario, "registro_id", "nombre", rMedico_Calendario.calendario_id);
            ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion", rMedico_Calendario.medico_id);
            return View(rMedico_Calendario);
        }

        // GET: rMedico_Calendario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //rMedico_Calendario rMedico_Calendario = await db.rMedico_Calendario.FindAsync(id);
            rMedico_Calendario rMedico_Calendario = await db.rMedico_Calendario.FindAsync(id);
            if (rMedico_Calendario == null)
            {
                return HttpNotFound();
            }
            ViewBag.calendario_id = new SelectList(db.mCalendario, "registro_id", "nombre", rMedico_Calendario.calendario_id);
            ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion", rMedico_Calendario.medico_id);
            return View(rMedico_Calendario);
        }

        // POST: rMedico_Calendario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,medico_id,calendario_id")] rMedico_Calendario rMedico_Calendario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rMedico_Calendario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.calendario_id = new SelectList(db.mCalendario, "registro_id", "nombre", rMedico_Calendario.calendario_id);
            ViewBag.medico_id = new SelectList(db.mPersona, "registro_id", "identificacion", rMedico_Calendario.medico_id);
            return View(rMedico_Calendario);
        }

        // GET: rMedico_Calendario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rMedico_Calendario rMedico_Calendario = await db.rMedico_Calendario.FindAsync(id);
            if (rMedico_Calendario == null)
            {
                return HttpNotFound();
            }
            return View(rMedico_Calendario);
        }

        // POST: rMedico_Calendario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rMedico_Calendario rMedico_Calendario = await db.rMedico_Calendario.FindAsync(id);
            db.rMedico_Calendario.Remove(rMedico_Calendario);
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
