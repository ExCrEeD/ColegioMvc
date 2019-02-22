using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colegio.Models
{
    public class Materias
    {
        [Key]
        public int IdMateria { set; get; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido ")]
        public String nombre { set; get; }
        
    }
}