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
using Rotativa;

namespace NTSIH.Controllers
{
    public class mCitasController : Controller
    {
        private SIHEntities db = new SIHEntities();

        

        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            //db.Catalogo.Where(d => d.catalogo_id == 1),
            var mCita = db.mCita.Include(m => m.Catalogo).Include(m => m.mAtencion).Include(m => m.mPersona).Include(m => m.mPersona1);


            ViewBag.especialidad_id = "1";
            //            select* from mPersona a
            //join rMedico_Especialidad b on a.registro_id = b.medico_id
            //join Catalogo c on b.especialidad_id = c.registro_id
            //where c.registro_id = 23





            var entryPoint = (from aa in db.mPersona
                              join b in db.rMedico_Especialidad on aa.registro_id equals b.medico_id 
                              join cc in db.Catalogo on b.especialidad_id equals cc.registro_id
                              where cc.registro_id == 23
                              select new
                              {                        
                              }).Take(10);


            //ViewBag.genero = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 2), "registro_id", "nombre");
            //TempData["especialidad"] = especialidadSelectList;


            return View(await mCita.ToListAsync());
        }

        // GET: mCitas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCita mCita = await db.mCita.FindAsync(id);
            if (mCita == null)
            {
                return HttpNotFound();
            }
            return View(mCita);
        }

        // GET: mCitas/Create
        public ActionResult Create()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            
            
            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 6), "registro_id", "nombre");
            ViewBag.registro_id = new SelectList(db.mAtencion, "cita_id", "diagnostico");

            //select* from rRol_Persona a
            //join mPersona b on a.persona_id = b.registro_id
            //where a.rol_id = 10
            ViewBag.paciente_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 10).Select(x => x.mPersona), "registro_id", "nombres");

            //select* from rRol_Persona a
            //join mPersona b on a.persona_id = b.registro_id
            //where a.rol_id = 15
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres");



            return View();
        }

        // POST: mCitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_id,paciente_id,medico_id,especialidad_id,fecha,hora,motivo,estado")] mCita mCita)
        {
            if (ModelState.IsValid)
            {
                mCita.motivo = mCita.motivo.ToUpper();
                db.mCita.Add(mCita);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 6), "registro_id", "nombre", mCita.especialidad_id);
            ViewBag.registro_id = new SelectList(db.mAtencion, "cita_id", "diagnostico", mCita.registro_id);
            ViewBag.paciente_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 10).Select(x => x.mPersona), "registro_id", "nombres", mCita.paciente_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", mCita.medico_id);
            return View(mCita);
        }

        // GET: mCitas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCita mCita = await db.mCita.FindAsync(id);
            if (mCita == null)
            {
                return HttpNotFound();
            }
            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 6), "registro_id", "nombre", mCita.especialidad_id);
            ViewBag.registro_id = new SelectList(db.mAtencion, "cita_id", "diagnostico", mCita.registro_id);
            ViewBag.paciente_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 10).Select(x => x.mPersona), "registro_id", "nombres", mCita.paciente_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", mCita.medico_id);
            return View(mCita);
        }

        // POST: mCitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_id,paciente_id,medico_id,especialidad_id,fecha,hora,motivo,estado")] mCita mCita)
        {
            if (ModelState.IsValid)
            {
                mCita.motivo = mCita.motivo.ToUpper();
                db.Entry(mCita).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.especialidad_id = new SelectList(db.Catalogo.Where(d => d.catalogo_id == 6), "registro_id", "nombre", mCita.especialidad_id);
            ViewBag.registro_id = new SelectList(db.mAtencion, "cita_id", "diagnostico", mCita.registro_id);
            ViewBag.paciente_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 10).Select(x => x.mPersona), "registro_id", "nombres", mCita.paciente_id);
            ViewBag.medico_id = new SelectList(db.rRol_Persona.Include("mPersona").Where(x => x.rol_id == 15).Select(x => x.mPersona), "registro_id", "nombres", mCita.medico_id);
            return View(mCita);
        }

        // GET: mCitas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mCita mCita = await db.mCita.FindAsync(id);
            if (mCita == null)
            {
                return HttpNotFound();
            }
            return View(mCita);
        }

        // POST: mCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            mCita mCita = await db.mCita.FindAsync(id);
            db.mCita.Remove(mCita);
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
