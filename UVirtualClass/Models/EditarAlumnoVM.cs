﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UVirtualClass.Models
{
    public class EditarAlumnoVM
    {

        [Required(ErrorMessage = "El Campo Usuario Esta Vacio")]
        [DisplayName("Usuario: ")]
        [MinLength(5, ErrorMessage = "El usuario debe tener al menos 5 Caracteres")]
        [MaxLength(60, ErrorMessage = "El usuario no debe exceder los 60 Caracteres")]
        public string Usuario1 { get; set; }
        [Required(ErrorMessage = "El Campo Correo no puede estar Vacio")]
        [EmailAddress(ErrorMessage = "El campo correo debe seguir la siguiente estructura Nombre@Example.com")]
        [DisplayName("Correo: ")]
        public string correo { get; set; }
        [Required(ErrorMessage = "El Campo Contraseña Esta Vacio")]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña: ")]
        public string contraseña { get; set; }
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("contraseña", ErrorMessage = "Las Contraseñas no Coinciden")]
        [DisplayName("Confirmar Contraseña: ")]
        public string ConfirmarContraseña { get; set; }
        [Required(ErrorMessage = "Este Campo Esta Vacio")]
        [DisplayName("Tipo de Usuario: ")]
        public int tipo { get; set; }
        [Required(ErrorMessage = "Este Campo Esta Vacio")]
        [DisplayName("Nombre Alumno: ")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Este Campo Esta Vacio")]
        [DisplayName("Apellidos del Alumno: ")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "Este Campo Esta Vacio")]
        [DisplayName("Fecha de Nacimiento: ")]
        [DataType(DataType.Date)]
        public DateTime fecha_n { get; set; }
        [Required(ErrorMessage = "Seleccione un Genero")]
        [DisplayName("Genero: ")]
        public char genero { get; set; }
        public int idUsuario { get; set; }

    }
}