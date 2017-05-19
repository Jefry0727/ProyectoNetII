using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Platform.Entity.Entity;
using Platform.Entity.DAO;

namespace ProyectoFinalNetII.Controllers
{
    public class ActividadController : Controller
    {
        private EntityEntities db = new EntityEntities();
        private daoDirector dao = new daoDirector();

        // GET: /Actividad/
        public ActionResult Index()
        {
            int idPro = (int)(Session["idProyecto"]);
            List<Actividad> actividades = dao.listaActividad(idPro);
            return View(actividades);
        }

        // GET: /Actividad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = db.Actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // GET: /Actividad/Create
        public ActionResult Create()
        {
            ViewBag.Integrante_id = new SelectList(db.Integrante, "id", "id");
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre");
            return View();
        }

        // POST: /Actividad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,fecha_inicio,fecha_fin,descripcion,Proyecto_id,Integrante_id")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                TimeSpan dato = actividad.fecha_fin - actividad.fecha_inicio;
                if (dato.Days >= 0)
                {
                    bool resp = dao.integranteActividad(actividad.Integrante_id);
                    if (resp)
                    {
                        int idPro = (int)(Session["idProyecto"]);
                        db.crearActividad(actividad.nombre, actividad.fecha_inicio, actividad.fecha_fin, actividad.descripcion,
                            idPro, actividad.Integrante_id);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }


            }

            ViewBag.Integrante_id = new SelectList(db.Integrante, "id", "id", actividad.Integrante_id);
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", actividad.Proyecto_id);
            return View(actividad);
        }

        // GET: /Actividad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = db.Actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            ViewBag.Integrante_id = new SelectList(db.Integrante, "id", "id", actividad.Integrante_id);
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", actividad.Proyecto_id);
            return View(actividad);
        }

        // POST: /Actividad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,fecha_inicio,fecha_fin,descripcion,Proyecto_id,Integrante_id")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                TimeSpan dato = actividad.fecha_fin - actividad.fecha_inicio;
                if (dato.Days >= 0)
                {
                    bool resp = dao.integranteActividad(actividad.Integrante_id);
                    if (resp)
                    {
                        int idPro = (int)(Session["idProyecto"]);
                        db.editarActividad(actividad.id, actividad.nombre, actividad.fecha_inicio, actividad.fecha_fin, actividad.descripcion,
                            idPro, actividad.Integrante_id);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }



            }
            ViewBag.Integrante_id = new SelectList(db.Integrante, "id", "id", actividad.Integrante_id);
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", actividad.Proyecto_id);
            return View(actividad);
        }

        // GET: /Actividad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = db.Actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // POST: /Actividad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool resp = dao.verificarActividad(id);
            if (resp)
            {
                db.eliminarActividad(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

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
