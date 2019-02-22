using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Models
{
    public class Asignaturas
    {
        [Key]
        public int IdAsignaturas { set; get; }
        public float nota1 { set; get; }
        public float nota2 { set; get; }
        public int idMateria { set; get; }
        public int idperson { set; get; }
        public int idprofesor { set; get; }
    }
}