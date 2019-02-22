using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Colegio.Context;
using Colegio.Models;
using System.Net;

namespace Colegio.Controllers
{
    public class EstudianteController : Controller
    {
        private DataStore db = new DataStore();
        // GET: Estudiante
        public ActionResult Index()
        {
            return View(db.estudiantes.ToList());
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else{
                estudiante x = db.estudiantes.Find(id);
                if (x == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else {
                    return View(x);
                }   
            }
            
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        [HttpPost]
        public ActionResult Create(estudiante student)
        {
            try
            {
               ViewBag.error = null;
               var reigstro = from a in db.estudiantes
                               where a.nombre == student.nombre && a.apellido == student.apellido 
                               select new
                               {
                                   a.nombre,
                                   a.apellido
                               };

                if (reigstro.Count() == 0)
                {  
                    db.estudiantes.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "ya existe el estudiante "+reigstro.First().nombre+" "+reigstro.First().apellido;
                    return View(student);
                }                            
            }
            catch
            {
                return View(student);
            }
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                estudiante x = db.estudiantes.Find(id);
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

        // POST: Estudiante/Edit/5
        [HttpPost]
        public ActionResult Edit(estudiante student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);
                
            }
            catch
            {
                return View(student);
            }
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                estudiante x = db.estudiantes.Find(id);
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

        // POST: Estudiante/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, estudiante student)
        {
            try
            {
                student = db.estudiantes.Find(id);
                db.estudiantes.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(student);
            }
        }
    }
}
