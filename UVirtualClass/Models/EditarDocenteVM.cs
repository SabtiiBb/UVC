using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UVirtualClass.Models
{
    public class EditarDocenteVM
    {

        public int idUsuario { get; set; }
        public String Usuario1 { get; set; }
        public String correo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String contraseña { get; set; }
        [DataType(DataType.Password)]
        [Compare("contraseña", ErrorMessage = "Las Contraseñas no Coinciden")]
        public String ConfirmarContraseña { get; set; }
        public int tipo { get; set; }
        [Required(ErrorMessage = "El Nombre es Requerido")]
        public String nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es Requerido")]
        public String apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecha_n { get; set; }
        public char genero { get; set; }

    }
}