﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UVirtualClass.Models
{
    public class CrearDocenteVM
    {

        public String Usuario1 { get; set; }
        public String correo { get; set; }
        public String contraseña { get; set; }
        public String ConfirmarContraseña { get; set; }
        public int tipo { get; set; }
        [Required(ErrorMessage = "el siguiente dato es requerido")]
        public String nombre { get; set; }
        public String apellido { get; set; }
        public DateTime fecha_n { get; set; }
        public char genero { get; set; }

    }
}