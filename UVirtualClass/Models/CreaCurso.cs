using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UVirtualClass.Models
{
    public class CreaCurso
    {
        [Required(ErrorMessage = "El campo nombre está vacio")]
        [DisplayName("Nombre: ")]
        [MaxLength(30, ErrorMessage = "El nombre no debe exceder los 30 caracter")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El campo descripcion está vacio")]
        [MaxLength(200, ErrorMessage = "La descripcion no debe exceder los 200 caracter")]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "El campo recursos está vacio")]
        [MaxLength(30, ErrorMessage = "El recurso no debe exceder los 30 caracter")]
        public String Recursos { get; set; }

        [Required(ErrorMessage = "El campo costo está vacio")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "El campo temario está vacio")]
        public int idTemario { get; set; }

        [Required(ErrorMessage = "El campo docente está vacio")]
        public int idDocente { get; set; }

        [Required(ErrorMessage = "El campo Foto está vacio")]
        public String Foto { get; set; }

    }
}