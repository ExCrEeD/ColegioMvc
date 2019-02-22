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
    public class NotasController : Controller
    {
        // GET: Notas
        private DataStore db = new DataStore();
        public ActionResult Index()
        {
            if (db.Asignaturas.Count() == 0)
            {
                ViewBag.message = "No se han asignado materias";
                return View();
            }
            else
            {
                ViewBag.message = " ";
                return View(db.estudiantes.ToList());
            }
            
        }

        // GET: Notas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notas/Create
        public ActionResult editNotas(Asignaturas asig, int? id,string nombremateria)
        {
            ViewBag.nombremateria = nombremateria;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                asig = db.Asignaturas.Find(id);            
                if (asig == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {                    
                    return View(asig);
                }
            }
        }

        // POST: Notas/Create
        [HttpPost]
        public ActionResult editNotas(Asignaturas asig, String idestudiante, int idmateria,int idprof)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    asig.idprofesor = idprof;
                    asig.idperson = Int32.Parse(idestudiante);
                    asig.idMateria = idmateria;
                    db.Entry(asig).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(asig);

            }
            catch
            {
                return View(asig);
            }
        }

        // GET: Notas/Edit/5
        public ActionResult Edit(int id)
        {

            var join = from a in db.Asignaturas  
                          join b in db.materias on a.idMateria equals b.IdMateria 
                          where a.idperson == id 
                          select new Join
                          {          
                             asignaturassVm = a,materiasvm =b
                          };
            ViewBag.idestudiante = id;
            return View(join);
        }

        // POST: Notas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
