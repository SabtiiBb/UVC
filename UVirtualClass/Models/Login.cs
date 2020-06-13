using UVirtualClass.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UVirtualClass.Models
{
    public class Login
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "<font color= 'red'> No puede dejar este espacio en blanco </font>")]
        [Display(Name = "Correo")]
        public String correo { get; set; }

        [StringLength(100, ErrorMessage = "el número de caracteres de {0} debe ser al menos {2}. </font>", MinimumLength = 3)]
        [Required(ErrorMessage = "<font color= 'red'> No puede dejar este espacio en blanco </font>")]
        [Display(Name = "Contraseña")]
        public String contraseña { get; set; }

        public String Usuario { get; set; }

        public String tipo { get; set; }

        ContextDbDataContext db = new ContextDbDataContext();

        public bool Validacion()
        {
            var query = from a in db.Usuario
                        where a.correo == correo && a.contraseña == contraseña && a.Activo == 1
                        select a;

            if (query.Count() > 0)
            {
                //var query2 = from a in db.Usuario where a.correo == correo select a;
                var datos = query.ToList();

                foreach (var Data in datos)
                {
                    Usuario = Data.Usuario1;
                    IdUsuario = Data.IdUsuario;
                    tipo = Data.tipo.ToString();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
