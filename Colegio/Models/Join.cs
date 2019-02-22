using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models
{
    public class Join
    {
        public Asignaturas asignaturassVm { get; set; }
        public AsignaturasProfesores asignaturasProfesoresVm { get; set; }
        public Materias materiasvm { get; set; }
        public estudiante estudiantevm { get; set; }
        public Profesor profesorvm { get; set; }
    }
}