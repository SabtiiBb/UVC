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

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DisplayName("Usuario: ")]
        [MinLength(5, ErrorMessage = "El usuario debe tener al menos 5 Caracteres")]
        [MaxLength(60, ErrorMessage = "El usuario no debe exceder los 60 Caracteres")]
        public String Usuario1 { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [EmailAddress(ErrorMessage = "El campo correo debe seguir la siguiente estructura Nombre@Example.com")]
        [DisplayName("Correo: ")]
        public String correo { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña: ")]
        public String contraseña { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("contraseña", ErrorMessage = "Las Contraseñas no Coinciden")]
        [DisplayName("Confirmar Contraseña: ")]
        public String ConfirmarContraseña { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DisplayName("Tipo de Usuario: ")]
        public int tipo { get; set; }

        [DisplayName("Nombre Docente: ")]
        [Required(ErrorMessage = "El siguiente dato es requerido")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DisplayName("Apellidos del Docente: ")]
        public String apellido { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DisplayName("Fecha de Nacimiento: ")]
        [DataType(DataType.Date)]
        public DateTime fecha_n { get; set; }

        [Required(ErrorMessage = "El siguiente dato es requerido")]
        [DisplayName("Genero: ")]
        public char genero { get; set; }

    }
}