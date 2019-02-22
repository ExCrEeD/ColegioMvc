using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colegio.Models
{
    public class Profesor
    {
        [Key]
        public int IdProfesor { set; get; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public String nombre { set; get; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public String apellido { set; get; }
        [Display(Name = "Numero de documento")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string numdoc { set; get; }
    }
}