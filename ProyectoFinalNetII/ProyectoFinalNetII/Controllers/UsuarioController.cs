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
    public class UsuarioController : Controller
    {
        private EntityEntities db = new EntityEntities();
        private daoDirector daoDir = new daoDirector();
        bool resp;

        // GET: /Usuario/
        public ActionResult Index()
        {
            String usu = (string)(Session["Usuario"]);
            List<Usuario> u = daoDir.listarUsu(usu);
            return View(u);
        }

        // GET: /Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            ViewBag.Tipo_Usuario = new SelectList(db.Tipo_Usuario, "id", "tipo");
            return View();
        }

        // POST: /Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,cedula,nombre,apellido,edad,telefono,usuario1,contrasenia,correo,Tipo_Usuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                String usu = (string)(Session["Usuario"]);
                String pass = (string)(Session["Contrasenia"]);

                resp = daoDir.verificarUsuario(usu);

                if (resp)
                {
                    db.registrarUsuDirector(usuario.cedula, usuario.nombre, usuario.apellido, usuario.edad,
                    usuario.telefono, usu, pass, 1, usuario.correo);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Tipo_Usuario = new SelectList(db.Tipo_Usuario, "id", "tipo", usuario.Tipo_Usuario);
            return View(usuario);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Usuario = new SelectList(db.Tipo_Usuario, "id", "tipo", usuario.Tipo_Usuario);
            return View(usuario);
        }

        // POST: /Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,cedula,nombre,apellido,edad,telefono,usuario1,contrasenia,correo,Tipo_Usuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {                
                String usu = (string)(Session["Usuario"]);
                String pass = (string)(Session["Contrasenia"]);

                usuario.usuario1 = usu;
                usuario.contrasenia = pass;
                usuario.Tipo_Usuario = 2;

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_Usuario = new SelectList(db.Tipo_Usuario, "id", "tipo", usuario.Tipo_Usuario);
            return View(usuario);
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
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
