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
    public class ProyectoController : Controller
    {
        private EntityEntities db = new EntityEntities();

        private daoDirector dao = new daoDirector();

        int idDire;
        List<Proyecto> proys = new List<Proyecto>();

        // GET: /Proyecto/
        public ActionResult Index()
        {
            String usu = (string)(Session["Usuario"]);
            idDire = dao.proyectosDirector(usu);
            List<Proyecto> proys = dao.listaProyectos(idDire);
            foreach (var a in proys)
            {
                Session["idProyecto"] = a.id;
            }
            return View(proys);
        }

        // GET: /Proyecto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: /Proyecto/Create
        public ActionResult Create()
        {
            ViewBag.Usuario_id = new SelectList(db.Usuario, "id", "cedula");
            return View();
        }

        // POST: /Proyecto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,fecha_inicio,fecha_fin,etapa,Usuario_id")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                TimeSpan dato = proyecto.fecha_fin - proyecto.fecha_inicio;
                if (dato.Days >= 0)
                {
                    String usu = (string)(Session["Usuario"]);
                    idDire = dao.proyectosDirector(usu);
                    bool resp = dao.verificarPosibleProyecto(idDire);
                    if (idDire != 0 && resp)
                    {
                        db.crearProyecto(proyecto.nombre, proyecto.fecha_inicio, proyecto.fecha_fin,
                        proyecto.etapa, idDire);
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

            ViewBag.Usuario_id = new SelectList(db.Usuario, "id", "cedula", proyecto.Usuario_id);
            return View(proyecto);
        }

        // GET: /Proyecto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario_id = new SelectList(db.Usuario, "id", "cedula", proyecto.Usuario_id);
            return View(proyecto);
        }

        // POST: /Proyecto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,fecha_inicio,fecha_fin,etapa,Usuario_id")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                String usu = (string)(Session["Usuario"]);
                idDire = dao.proyectosDirector(usu);
                db.editarProyecto(proyecto.id, proyecto.nombre, proyecto.fecha_inicio, proyecto.fecha_fin,
                    proyecto.etapa, idDire);
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_id = new SelectList(db.Usuario, "id", "cedula", proyecto.Usuario_id);
            return View(proyecto);
        }

        // GET: /Proyecto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyecto.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: /Proyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool resp = dao.verificarProyecto(id);
            bool resp2 = dao.verificarProyectoCargo(id);
            bool resp3 = dao.verificarProyectoIntegra(id);
            bool resp4 = dao.verificarProyectoReunion(id);

            if (resp && resp2 && resp3 && resp4)
            {
                db.eliminarProyecto(id);
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
