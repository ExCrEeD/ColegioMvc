using Colegio.Context;
using Colegio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class ProfesorController : Controller
    {
        private DataStore db = new DataStore();
        // GET: Profesor
        public ActionResult Index()
        {
            return View(db.profesores.ToList());
        }

        // GET: Profesor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Profesor x = db.profesores.Find(id);
                if (x == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(x);
                }
            }
        }

        // GET: Profesor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profesor/Create
        [HttpPost]
        public ActionResult Create(Profesor maestro)
        {
            try
            {
                ViewBag.error = null;
                var reigstro = from a in db.profesores
                               where a.nombre == maestro.nombre && a.apellido == maestro.apellido
                               select new
                               {
                                   a.nombre,
                                   a.apellido
                               };
                if (reigstro.Count() == 0)
                {
                    db.profesores.Add(maestro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "ya existe el estudiante " + reigstro.First().nombre + " " + reigstro.First().apellido;
                    return View(maestro);
                }
            }
            catch
            {
                return View(maestro);
            }
        }

        // GET: Profesor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Profesor x = db.profesores.Find(id);
                if (x == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(x);
                }
            }
        }

        // POST: Profesor/Edit/5
        [HttpPost]
        public ActionResult Edit(Profesor maestro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(maestro).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(maestro);

            }
            catch
            {
                return View(maestro);
            }
        }

            // GET: Profesor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Profesor x = db.profesores.Find(id);
                if (x == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(x);
                }
            }
        }

        // POST: Profesor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Profesor maestro)
        {
            try
            {
                maestro = db.profesores.Find(id);
                db.profesores.Remove(maestro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(maestro);
            }
        }
    }
 }

