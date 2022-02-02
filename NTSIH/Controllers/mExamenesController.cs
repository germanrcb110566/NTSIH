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
    public class mExamenesController : Controller
    {
        private SIHEntities db = new SIHEntities();



        public ActionResult HeaderPDF()
        {
            return View("HeaderPDF");
        }

        public ActionResult FooterPDF()
        {
            return View("FooterPDF");
        }


        
        public async Task<ActionResult> Print(int? id)
        {
            // Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDF", "mExamenes", null, "http");
            // Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "mExamenes", null, "http");
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            var mTratamiento = db.mTratamiento.Where(m=>m.cita_id==id).Include(m => m.Catalogo).Where(x => x.Catalogo.catalogo_id == 11).Include(m => m.mCita);
            string Mensaje="";
            mPersona oUser = EncDecryptController.unRegistroId(ref  Mensaje,  id);
            ViewBag.doctornombre = oUser.nombres;
            ViewBag.doctorapellido = oUser.apellidos;
            ViewBag.doctortelefono = oUser.telefono;



            //return View(await mTratamiento.ToListAsync());



            //return new ViewAsPdf("Print", await mCita.ToListAsync());

            return new ViewAsPdf("Print", await mTratamiento.ToListAsync())
            {
                // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 0 " +
                                 "--footer-html " + _footerUrl + " --footer-spacing 0"
                ,
                PageSize = Rotativa.Options.Size.A4
                //,FileName = "CustomersLista.pdf" // SI QUEREMOS QUE EL ARCHIVO SE DESCARGUE DIRECTAMENTE
                ,
                PageMargins = new Rotativa.Options.Margins(40, 10, 10, 10)
            };
        }

        // GET: mExamenes
        public async Task<ActionResult> Index()
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

            var mTratamiento = db.mTratamiento.Include(m => m.Catalogo).Where(x => x.Catalogo.catalogo_id == 11).Include(m => m.mCita);
            return View(await mTratamiento.ToListAsync());
        }

        // GET: mExamenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
            ViewBag.alerta = "success";
            ViewBag.acceso = "Acceso A:".ToUpper() + Session["Nombres"] + "........ASIGNADO EL ROL:" + Session["Rol"];
            ViewBag.layout = Session["Layout"];

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
