using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Colegio.Models;

namespace Colegio.Context
{
    public class DataStore:DbContext
    {
        public DbSet<estudiante> estudiantes { set; get; }
        public DbSet<Materias> materias { set; get; }
        public DbSet<Profesor> profesores { set; get; }
        public DbSet<Asignaturas> Asignaturas { set; get; }
        public DbSet<AsignaturasProfesores> AsignaturasProfesores { set; get; }
    }
}