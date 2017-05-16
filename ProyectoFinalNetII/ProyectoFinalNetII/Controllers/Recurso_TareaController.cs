using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Platform.Entity.Entity;

namespace ProyectoFinalNetII.Controllers
{
    public class Recurso_TareaController : Controller
    {
        private EntityEntities db = new EntityEntities();

        // GET: /Recurso_Tarea/
        public ActionResult Index()
        {
            var recurso_tarea = db.Recurso_Tarea.Include(r => r.Actividad).Include(r => r.Recurso).Include(r => r.Tarea);
            return View(recurso_tarea.ToList());
        }

        // GET: /Recurso_Tarea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso_Tarea recurso_tarea = db.Recurso_Tarea.Find(id);
            if (recurso_tarea == null)
            {
                return HttpNotFound();
            }
            return View(recurso_tarea);
        }

        // GET: /Recurso_Tarea/Create
        public ActionResult Create()
        {
            ViewBag.Actividad_id = new SelectList(db.Actividad, "id", "nombre");
            ViewBag.Recurso_id = new SelectList(db.Recurso, "id", "nombre");
            ViewBag.Tarea_id = new SelectList(db.Tarea, "id", "nombre");
            return View();
        }

        // POST: /Recurso_Tarea/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,cantidad,Actividad_id,Tarea_id,Recurso_id")] Recurso_Tarea recurso_tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.descontarRecurso(recurso_tarea.cantidad, recurso_tarea.Recurso_id);
                    db.createRecursoTarea(recurso_tarea.cantidad, recurso_tarea.Actividad_id, recurso_tarea.Tarea_id, recurso_tarea.Recurso_id);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }  
            }

            ViewBag.Actividad_id = new SelectList(db.Actividad, "id", "nombre", recurso_tarea.Actividad_id);
            ViewBag.Recurso_id = new SelectList(db.Recurso, "id", "nombre", recurso_tarea.Recurso_id);
            ViewBag.Tarea_id = new SelectList(db.Tarea, "id", "nombre", recurso_tarea.Tarea_id);
            return View(recurso_tarea);
        }

        // GET: /Recurso_Tarea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso_Tarea recurso_tarea = db.Recurso_Tarea.Find(id);
            if (recurso_tarea == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actividad_id = new SelectList(db.Actividad, "id", "nombre", recurso_tarea.Actividad_id);
            ViewBag.Recurso_id = new SelectList(db.Recurso, "id", "nombre", recurso_tarea.Recurso_id);
            ViewBag.Tarea_id = new SelectList(db.Tarea, "id", "nombre", recurso_tarea.Tarea_id);
            return View(recurso_tarea);
        }

        // POST: /Recurso_Tarea/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,cantidad,Actividad_id,Tarea_id,Recurso_id")] Recurso_Tarea recurso_tarea)
        {
            if (ModelState.IsValid)
            {
                db.descontarRecurso(recurso_tarea.cantidad, recurso_tarea.Recurso_id);
                db.editarRecursoTarea(recurso_tarea.id, recurso_tarea.cantidad, recurso_tarea.Actividad_id, recurso_tarea.Tarea_id, recurso_tarea.Recurso_id);
                return RedirectToAction("Index");
            }
            ViewBag.Actividad_id = new SelectList(db.Actividad, "id", "nombre", recurso_tarea.Actividad_id);
            ViewBag.Recurso_id = new SelectList(db.Recurso, "id", "nombre", recurso_tarea.Recurso_id);
            ViewBag.Tarea_id = new SelectList(db.Tarea, "id", "nombre", recurso_tarea.Tarea_id);
            return View(recurso_tarea);
        }

        // GET: /Recurso_Tarea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso_Tarea recurso_tarea = db.Recurso_Tarea.Find(id);
            if (recurso_tarea == null)
            {
                return HttpNotFound();
            }
            return View(recurso_tarea);
        }

        // POST: /Recurso_Tarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.eliminarRecursoTarea(id);
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
