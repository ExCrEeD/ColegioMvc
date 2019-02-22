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
    public class MateriaController : Controller
    {
        private DataStore db = new DataStore();
        // GET: Materia
        public ActionResult Index()
        {
            return View(db.materias.ToList());
        }

        // GET: Materia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Materias x = db.materias.Find(id);
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

        // GET: Materia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materia/Create
        [HttpPost]
        public ActionResult Create(Materias materia)
        {
            try
            {
                var reigstro = from a in db.materias
                               where a.nombre == materia.nombre
                           select new
                           {
                               a.nombre
                           };

                if (reigstro.Count() == 0)
                {
                    db.materias.Add(materia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Ya existe una materia con el nombre de "+ reigstro.First().nombre;
                    return View(materia);
                }                
            }
            catch
            {
                return View(materia);
            }
        }

        // GET: Materia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Materias x = db.materias.Find(id);
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

        // POST: Materia/Edit/5
        [HttpPost]
        public ActionResult Edit(Materias materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(materia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(materia);

            }
            catch
            {
                return View(materia);
            }
        }

        // GET: Materia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Materias x = db.materias.Find(id);
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

        // POST: Materia/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Materias materia)
        {
            try
            {
                    materia = db.materias.Find(id);
                    db.materias.Remove(materia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
              }
            catch
            {
                return View(materia);
            }
        }
    }
}

