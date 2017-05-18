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
    public class CargoController : Controller
    {
        private EntityEntities db = new EntityEntities();
        private daoDirector dao = new daoDirector();

        // GET: /Cargo/
        public ActionResult Index()
        {
            int idPro = (int)(Session["idProyecto"]);
            List<Cargo> cargos = dao.listaCargos(idPro);
            return View(cargos);
        }

        // GET: /Cargo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: /Cargo/Create
        public ActionResult Create()
        {
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre");
            return View();
        }

        // POST: /Cargo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,nombre,salario,horario,Proyecto_id")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                int idPro = (int)(Session["idProyecto"]);
                db.crearCargo(cargo.nombre, cargo.salario, cargo.horario, idPro);
                return RedirectToAction("Index");
            }

            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", cargo.Proyecto_id);
            return View(cargo);
        }

        // GET: /Cargo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", cargo.Proyecto_id);
            return View(cargo);
        }

        // POST: /Cargo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,nombre,salario,horario,Proyecto_id")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                int idPro = (int)(Session["idProyecto"]);
                cargo.Proyecto_id = idPro;
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Proyecto_id = new SelectList(db.Proyecto, "id", "nombre", cargo.Proyecto_id);
            return View(cargo);
        }

        // GET: /Cargo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: /Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool resp = dao.verificarCargoIntegrante(id);
            if(resp){
                Cargo cargo = db.Cargo.Find(id);
                db.Cargo.Remove(cargo);
                db.SaveChanges();
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
