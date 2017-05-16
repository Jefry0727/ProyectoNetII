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
    public class RecursoController : Controller
    {
        private EntityEntities db = new EntityEntities();

        // GET: /Recurso/
        public ActionResult Index()
        {
            return View(db.Recurso.ToList());
        }

        // GET: /Recurso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        // GET: /Recurso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Recurso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,nombre,cantidad,ubicacion")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                db.crearRecurso(recurso.nombre, recurso.cantidad, recurso.ubicacion);
                return RedirectToAction("Index");
            }

            return View(recurso);
        }

        // GET: /Recurso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        // POST: /Recurso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,nombre,cantidad,ubicacion")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                db.editarRecurso(recurso.id, recurso.nombre, recurso.cantidad, recurso.ubicacion);
                return RedirectToAction("Index");
            }
            return View(recurso);
        }

        // GET: /Recurso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        // POST: /Recurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.eliminarRecurso(id);
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
