using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Colegio.Models
{
    public class AsignaturasProfesores
    {
        [Key]
        public int IdAsignaturasProfesores { set; get; }
        public String tipo { set; get; }
        public float nota1 { set; get; }
        public float nota2 { set; get; }
        public int idMateria { set; get; }
        public int idperson { set; get; }
    }
}