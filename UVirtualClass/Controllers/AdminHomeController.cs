using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UVirtualClass.DataContext;
using UVirtualClass.Models;

namespace UVirtualClass.Controllers
{
    public class AdminHomeController : Controller
    {

        ContextDbDataContext db = new ContextDbDataContext();

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaAlumnos(string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewAlumnos> ListadoAlumno = (from db in db.ViewAlumnos select db);

            return View(ListadoAlumno);
        }

        public ActionResult ListaDocente (string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewDocentes> ListadoDocentes = (from db in db.ViewDocentes select db);

            return View(ListadoDocentes);
        }

        public ActionResult ListaCurso(string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewCurso> ListadoCurso = (from db in db.ViewCurso select db);

            return View(ListadoCurso);
        }

        public ActionResult CrearDocente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearDocente(FormCollection c, Docentes model, Usuario modelo)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.InsertOnSubmit(modelo);
                db.Docentes.InsertOnSubmit(model);
                db.SubmitChanges();
                Usuario Id = (from db in db.Usuario select db).Last();
                Docentes Ids = (from db in db.Docentes select db).Last();
                db.SP_ModificaDocente(Ids.IdDocente, Ids.nombre, Ids.apellido, Ids.fecha_n, Ids.genero, Id.IdUsuario);
                db.SubmitChanges();
            }
            return View();
        }

        public ActionResult CrearAlumno()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Masculino", Value = "M" });
            lst.Add(new SelectListItem() { Text = "Femenino", Value = "F" });

            ViewBag.genero = lst;

            return View();
        }

        [HttpPost]
        public ActionResult CrearAlumno(FormCollection c, Alumno Alum, Usuario Usu)
        {
            Usu.tipo = 3;
            Usu.Activo = 1;
            string message;
            try
            {
                db.Usuario.InsertOnSubmit(Usu);
                db.SubmitChanges();
                IEnumerable<Usuario> LastUser = (from db in db.Usuario select db);

                var LastUser1 = LastUser.LastOrDefault();
                if (LastUser1 != null)
                {
                    Alum.idUsuario = LastUser1.IdUsuario;
                    Alum.Activo = 1;
                    db.Alumno.InsertOnSubmit(Alum);
                    db.SubmitChanges();
                }
                message = "Crear";
            }
            catch (Exception e)
            {
                var error = e;
                message = "Error";
            }
            return RedirectToAction("ListaAlumnos", "AdminHome", new { a = message });
        }


        [HttpPost]
        public ActionResult CrearCurso(FormCollection c, Cursos cur)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.InsertOnSubmit(cur);
                db.SubmitChanges();

            }
            return View();
        }

        public ActionResult CrearCurso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditaAlumno(int id)
        {
            CrearAlumnoVM Alum = new CrearAlumnoVM();

            using (var DataContext = new ContextDbDataContext())
            {
                Alumno DataAlum = (from db in DataContext.Alumno where db.IdAlumno == id select db).Single();
                Usuario DataUser = (from db in DataContext.Usuario where db.IdUsuario == DataAlum.idUsuario select db).Single();

                Alum.idUsuario = int.Parse(DataAlum.idUsuario.ToString());
                Alum.Usuario1 = DataAlum.Usuario.ToString();
                Alum.correo = DataUser.correo.ToString();
                Alum.nombre = DataAlum.nombre;
                Alum.apellido = DataAlum.apellido;
                Alum.contraseña = "DummyPass";
                Alum.ConfirmarContraseña = "DummyPass";
                Alum.fecha_n = Convert.ToDateTime(DataAlum.fecha_n);
                Alum.genero = Convert.ToChar(DataAlum.genero);
                Alum.tipo = int.Parse(DataUser.tipo.ToString());
            }

                return View(Alum);
        }

        public JsonResult EliminarAlumno(int idAlumno)
        {
            using (var dbContext = new ContextDbDataContext())
            {
                Alumno Alum = (from dbD in dbContext.Alumno where dbD.IdAlumno == idAlumno select dbD).Single();
                Usuario User = (from dbD in dbContext.Usuario where dbD.IdUsuario == Alum.idUsuario select dbD).Single();

                dbContext.SP_ModificaAlumno(Alum.IdAlumno, Alum.nombre, Alum.apellido, Alum.fecha_n, Alum.genero, 0);
                dbContext.SP_ModificaUsuario(User.IdUsuario, User.Usuario1, User.correo, User.contraseña, 0, User.tipo);
            }

            return Json( new{ exito = true }, JsonRequestBehavior.AllowGet );
        }



        //--------------------------- VALIDATIONS CLIENT'S SIDE ---------------------------
        [HttpPost]
        public JsonResult UserRequestValidation(string Usuario1)
        {
            return Json(UsuarioRegistrado(Usuario1));
        }



        public bool UsuarioRegistrado(string User)
        {
            var UsuRegis = (from db in db.Usuario
                            where db.Usuario1.ToUpper() == User.ToUpper()
                            select new { User }).FirstOrDefault();




            bool ifExist = UsuRegis != null ? false : true;



            return ifExist;
        }



        [HttpPost]
        public JsonResult EmailRequestValidation(string correo)
        {
            return Json(CorreoRegistrado(correo));
        }
        public bool CorreoRegistrado(string Email)
        {
            var EmailExist = (from db in db.Usuario
                              where db.correo.ToUpper() == Email.ToUpper()
                              select new { Email }).FirstOrDefault();
            
            bool IsValid = EmailExist != null ? false : true;
            return IsValid;
        }
    }
}