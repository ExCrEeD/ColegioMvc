using Colegio.Context;
using Colegio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Colegio.Controllers
{
    public class ASignaturasController : Controller
    {
        private DataStore db = new DataStore();        
        // GET: ASignaturas
        public ActionResult Index()
        {
            if (db.materias.Count() == 0)
            {
                ViewBag.Message = "no se han registrado materias";
                return View();
            }
            else
            {
                if (db.profesores.Count() == 0)
                {
                    ViewBag.Message = "no se han registrado profesores";       
                }
                else
                {
                    if (db.estudiantes.Count() == 0)
                    {
                        ViewBag.Message = "no se han registrado alumnos";
                    }
                    {
                        return View();
                    }                  
                }               
            }
            return View();
        }
        // GET: ASignaturas
        public ActionResult Index2()
        {          
            return View(db.profesores.ToList());
        }
        // GET: ASignaturas
        public ActionResult Index3()
        {
            if (db.AsignaturasProfesores.Count() == 0)
            {
                ViewBag.Message = "No se han asigando materias a profesores";
            }
            return View(db.estudiantes.ToList());
        }

        // GET: ASignaturas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ASignaturas/Create
        public ActionResult Create( Asignaturas asignaturas,string id)
        {
            //var getMateriasList  = db.materias.ToList();
            int id2 = Int32.Parse(id);
            var query = from a in db.AsignaturasProfesores
                       join b in db.materias on a.idMateria equals b.IdMateria
                       where !(from c in db.Asignaturas where c.idperson == id2 select c.idMateria).Contains(a.idMateria) 
                       select new 
                       {  
                           b.IdMateria,
                           b.nombre
                       };
            ViewBag.error = null;
            if (query.Count() == 0)
            {
                ViewBag.error = "No hay materias por asignar";
            }
            //SelectList list = new SelectList(getMateriasList, "idMateria", "Nombre");
            SelectList list = new SelectList(query.ToList(), "idMateria", "Nombre");
            ViewBag.materiaList = list;
            return View(asignaturas);
        }

        // POST: ASignaturas/Create
        [HttpPost]
        public ActionResult Create(Asignaturas asignature, String materiasList,String id,String tipo)
        {
            int strDDLValue = Int32.Parse(materiasList);
            var join = from a in db.AsignaturasProfesores
                       join b in db.materias on a.idMateria equals b.IdMateria
                       where strDDLValue == b.IdMateria
                       select new Join
                       {
                          asignaturasProfesoresVm = a
                       };
            asignature.idprofesor = join.First().asignaturasProfesoresVm.idperson;          
            asignature.idperson = Int32.Parse(id);
            asignature.idMateria = strDDLValue;   
            db.Asignaturas.Add(asignature);
            db.SaveChanges();
            return RedirectToAction("Index");            
        }

        // GET: ASignaturas/Create
        public ActionResult CreateProfesor(AsignaturasProfesores asignaturas)
        {
            var query = from a in db.materias
                        where !(from b in db.AsignaturasProfesores select b.idMateria).Contains(a.IdMateria) select a;
            var getMateriasList = query.ToList();
            SelectList list = new SelectList(getMateriasList, "idMateria", "Nombre");
            ViewBag.materiaList = list;
            ViewBag.error = null;
            if (query.Count() == 0)
            {
                ViewBag.error = "No hay materias por asignar";
            }
            return View(asignaturas);
        }

        // POST: ASignaturas/Create
        [HttpPost]
        public ActionResult CreateProfesor(AsignaturasProfesores asignature, String materiasList, String id, String tipo)
        {
            int strDDLValue = Int32.Parse(materiasList);
            asignature.idperson = Int32.Parse(id);
            asignature.idMateria = strDDLValue;
            db.AsignaturasProfesores.Add(asignature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: ASignaturas/Edit/5
        public ActionResult Edit(int id,string tipo)
        {
            ViewBag.profesorid = id.ToString();
            ViewBag.profesortipo = tipo;
            if (tipo.Equals("profesor"))
            {
                Profesor x = db.profesores.Find(id);
                ViewBag.profesor = x.nombre + " " + x.apellido;
                var join = from a in db.AsignaturasProfesores
                           join b in db.materias on a.idMateria equals b.IdMateria
                           where a.idperson == id 
                           select new Join
                           {
                               asignaturasProfesoresVm = a,
                               materiasvm = b
                           };
                return View(join);             
            }
           else
            {
                estudiante x = db.estudiantes.Find(id);
                ViewBag.profesor = x.nombre + " " + x.apellido;
                var join = from a in db.Asignaturas
                           join b in db.materias on a.idMateria equals b.IdMateria
                           where a.idperson == id 
                           select new Join
                           {
                               asignaturassVm = a,
                               materiasvm = b
                           };
                return View(join);
            }
            
        }

        // POST: ASignaturas/Edit/5
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

        // GET: ASignaturas/Delete/5
        public ActionResult Delete(int? id,String nombremateria)
        {
            ViewBag.nombremateria = nombremateria;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Asignaturas x = db.Asignaturas.Find(id);
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

        // POST: ASignaturas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Asignaturas asignature)
        {
            try
            {
                asignature = db.Asignaturas.Find(id);
                db.Asignaturas.Remove(asignature);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return View(asignature);
            }
        }

        // GET: ASignaturas/Delete/5
        public ActionResult DeleteProfesor(int? id,string nombremateria)
        {
            ViewBag.nombremateria = nombremateria;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                AsignaturasProfesores x = db.AsignaturasProfesores.Find(id);
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

        // POST: ASignaturas/Delete/5
        [HttpPost]
        public ActionResult DeleteProfesor(int id, AsignaturasProfesores asignature)
        {
            try
            {
                asignature = db.AsignaturasProfesores.Find(id);
                db.AsignaturasProfesores.Remove(asignature);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return View(asignature);
            }
        }
    }
}
