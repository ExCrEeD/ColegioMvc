using Colegio.Context;
using Colegio.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Colegio.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private DataStore db = new DataStore();
        public ActionResult Index()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            var join = from a in db.estudiantes
                       from b in db.materias
                       from c in db.Asignaturas
                       from d in db.profesores
                       where a.IdEstudiante == c.idperson && c.idMateria == b.IdMateria && d.IdProfesor == c.idprofesor
                       select new
                       {
                           nombre = a.nombre,
                           nombre1 = a.apellido,
                           nota1 = c.nota1,
                           nota2 = c.nota2,
                           nombre2 =d.nombre+" "+d.apellido
                            
                       };
            //var connectionString = ConfigurationManager.ConnectionStrings["Datastore"].ConnectionString;
            //String query = "select estudiantes.nombre,Materias.nombre,Asignaturas.nota1,Asignaturas.nota2,Profesors.nombre from estudiantes,Materias,Asignaturas,AsignaturasProfesores,Profesors where estudiantes.IdEstudiante = Asignaturas.idperson and asignaturas.idMateria = Materias.IdMateria  and Profesors.IdProfesor = Asignaturas.idprofesor";
            //SqlConnection conx = new SqlConnection(connectionString); SqlDataAdapter adp = new SqlDataAdapter(query, conx);
            //colegioDBDataSet ds = new colegioDBDataSet();
            //adp.Fill(ds, ds.Asignaturas.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report.rdlc";
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", join));


            ViewBag.ReportViewer = reportViewer;

            //var join = from a in db.estudiantes 
            //           from b in db.materias
            //           from c in db.Asignaturas
            //           from d in db.profesores
            //           where a.IdEstudiante == c.idperson && c.idMateria == b.IdMateria && d.IdProfesor ==c.idprofesor
            //           select new Join
            //           {
            //               estudiantevm =a,materiasvm=b,asignaturassVm=c,profesorvm=d
            //            };
            return View();
        }
    }
}