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
        //******************************ALUMNO'S METHODS**********************************
        public ActionResult ListaAlumnos(string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewAlumnos> ListadoAlumno = (from db in db.ViewAlumnos select db);

            return View(ListadoAlumno);
        }

        [HttpGet]
        public ActionResult CrearAlumno()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Masculino", Value = "M" });
            lst.Add(new SelectListItem() { Text = "Femenino", Value = "F" });

            ViewBag.genero = lst;

            return View();
        }

        [HttpPost]
        public ActionResult CrearAlumno(FormCollection c, CrearAlumnoVM MyModel)
        {

            string message = "AlumnoCreado";

            if (ModelState.IsValid)
            {
                using (var dbContext = new ContextDbDataContext())
                {
                    Usuario User = new Usuario();
                    Alumno Alum = new Alumno();

                    User.Usuario1 = MyModel.Usuario1;
                    User.contraseña = MyModel.contraseña;
                    User.Activo = 1;
                    User.tipo = 2;
                    dbContext.Usuario.InsertOnSubmit(User);
                    dbContext.SubmitChanges();

                    var Find = (from dbD in dbContext.Usuario select dbD).ToList();
                    User = Find.LastOrDefault();


                    Alum.nombre = MyModel.nombre;
                    Alum.apellido = MyModel.apellido;
                    Alum.fecha_n = Convert.ToDateTime(MyModel.fecha_n);
                    Alum.genero = Convert.ToChar(MyModel.genero);
                    Alum.idUsuario = User.IdUsuario;
                    dbContext.Alumno.InsertOnSubmit(Alum);
                    dbContext.SubmitChanges();

                }
            }
            else { message = "Error"; }

            return RedirectToAction("ListaAlumnos", "AdminHome", new { a = message });
        }

        [HttpGet]
        public ActionResult EditaAlumno(int id)
        {
            EditarAlumnoVM Alum = new EditarAlumnoVM();

            using (var DataContext = new ContextDbDataContext())
            {
                Alumno DataAlum = (from db in DataContext.Alumno where db.IdAlumno == id select db).Single();
                Usuario DataUser = (from db in DataContext.Usuario where db.IdUsuario == DataAlum.idUsuario select db).Single();

                Alum.idUsuario = int.Parse(DataAlum.idUsuario.ToString());
                Alum.Usuario1 = DataUser.Usuario1.ToString();
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

        [HttpPost]
        public ActionResult EditaAlumno(FormCollection e, EditarAlumnoVM MyModel)
        {
            string message = "AlumnoEditado";
            if (ModelState.IsValid)
            {
                using (var dbContext = new ContextDbDataContext())
                {
                    Alumno Alum = (from dbD in dbContext.Alumno where dbD.idUsuario == MyModel.idUsuario select dbD).Single();
                    Usuario User = (from dbD in dbContext.Usuario where dbD.IdUsuario == MyModel.idUsuario select dbD).Single();

                    User.contraseña = MyModel.contraseña == "DummyPass" ? User.contraseña : MyModel.contraseña;
                    User.Usuario1 = MyModel.Usuario1;
                    Alum.nombre = MyModel.nombre;
                    Alum.apellido = MyModel.apellido;
                    Alum.fecha_n = Convert.ToDateTime(MyModel.fecha_n);
                    Alum.genero = Convert.ToChar(MyModel.genero);

                    dbContext.SP_ModificaAlumno(Alum.IdAlumno, Alum.nombre, Alum.apellido, Alum.fecha_n, Alum.genero);
                    dbContext.SP_ModificarUsuario(User.IdUsuario, User.contraseña, User.Activo);
                    //dbContext.SubmitChanges();
                }
            }
            else { message = "Error"; }

            return RedirectToAction("ListaAlumnos", "AdminHome", new { a = message });
        }

        public JsonResult EliminarAlumno(int idAlumno)
        {
            using (var dbContext = new ContextDbDataContext())
            {
                Alumno Alum = (from dbD in dbContext.Alumno where dbD.IdAlumno == idAlumno select dbD).Single();
                Usuario User = (from dbD in dbContext.Usuario where dbD.IdUsuario == Alum.idUsuario select dbD).Single();

                //dbContext.SP_ModificarUsuario(User.IdUsuario, User.contraseña, 0);
                dbContext.SP_ModificaUsuario(User.IdUsuario, User.Usuario1, User.correo, User.contraseña, 0, User.tipo);
            }

            return Json(new { exito = true }, JsonRequestBehavior.AllowGet);
        }


        //******************************DOCENTE'S METHODS**********************************
        public ActionResult ListaDocente (string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewDocentes> ListadoDocentes = (from db in db.ViewDocentes select db);

            return View(ListadoDocentes);
        }

        [HttpGet]
        public ActionResult CrearDocente()
        {

            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Masculino", Value = "M" });
            lst.Add(new SelectListItem() { Text = "Femenino", Value = "F" });

            ViewBag.genero = lst;

            return View();
        }

        [HttpPost]
        public ActionResult CrearDocente(FormCollection c, CrearDocenteVM MyModel)
        {
            string message = "DocenteCreado";
            if (ModelState.IsValid)
            {
                using (var dbContext = new ContextDbDataContext())
                {
                    Usuario User = new Usuario();
                    Docentes Docen = new Docentes();

                    User.Usuario1 = MyModel.Usuario1;
                    User.contraseña = MyModel.contraseña;
                    User.Activo = 1;
                    User.tipo = 2;
                    dbContext.Usuario.InsertOnSubmit(User);
                    dbContext.SubmitChanges();

                    var Find = (from dbD in dbContext.Usuario select dbD).ToList();
                    User = Find.LastOrDefault();


                    Docen.nombre = MyModel.nombre;
                    Docen.apellido = MyModel.apellido;
                    Docen.fecha_n = Convert.ToDateTime(MyModel.fecha_n);
                    Docen.genero = Convert.ToChar(MyModel.genero);
                    Docen.idUsuario = User.IdUsuario;
                    dbContext.Docentes.InsertOnSubmit(Docen);
                    dbContext.SubmitChanges();

                }
            }
            else { message = "Error"; }
            return RedirectToAction("ListaDocente", "AdminHome", new { a = message });
        }

        [HttpGet]
        public ActionResult EditaDocente(int id)
        {
            EditarDocenteVM Docen = new EditarDocenteVM();

            using (var DataContext = new ContextDbDataContext())
            {
                Docentes DataDocen = (from db in DataContext.Docentes where db.IdDocente == id select db).Single();
                Usuario DataUser = (from db in DataContext.Usuario where db.IdUsuario == DataDocen.idUsuario select db).Single();

                Docen.idUsuario = int.Parse(DataDocen.idUsuario.ToString());
                Docen.Usuario1 = DataUser.Usuario1.ToString();
                Docen.correo = DataUser.correo.ToString();
                Docen.nombre = DataDocen.nombre;
                Docen.apellido = DataDocen.apellido;
                Docen.contraseña = "DummyPass";
                Docen.ConfirmarContraseña = "DummyPass";
                Docen.fecha_n = Convert.ToDateTime(DataDocen.fecha_n);
                Docen.genero = Convert.ToChar(DataDocen.genero);
                Docen.tipo = int.Parse(DataUser.tipo.ToString());
            }

            return View(Docen);
        }

        [HttpPost]
        public ActionResult EditaDocente(FormCollection e, EditarAlumnoVM MyModel)
        {
            string message = "AlumnoEditado";
            if (ModelState.IsValid)
            {
                using (var dbContext = new ContextDbDataContext())
                {
                    Docentes Docen = (from dbD in dbContext.Docentes where dbD.idUsuario == MyModel.idUsuario select dbD).Single();
                    Usuario User = (from dbD in dbContext.Usuario where dbD.IdUsuario == MyModel.idUsuario select dbD).Single();

                    User.contraseña = MyModel.contraseña == "DummyPass" ? User.contraseña : MyModel.contraseña;
                    User.Usuario1 = MyModel.Usuario1;
                    Docen.nombre = MyModel.nombre;
                    Docen.apellido = MyModel.apellido;
                    Docen.fecha_n = Convert.ToDateTime(MyModel.fecha_n);
                    Docen.genero = Convert.ToChar(MyModel.genero);

                    dbContext.SP_ModificaDocente(Docen.IdDocente, Docen.nombre, Docen.apellido, Docen.fecha_n, Docen.genero, Docen.idUsuario);
                    dbContext.SP_ModificarUsuario(User.IdUsuario, User.contraseña, User.Activo);
                    dbContext.SubmitChanges();
                }
            }
            else { message = "Error"; }

            return RedirectToAction("ListaAlumnos", "AdminHome", new { a = message });
        }

        public JsonResult EliminarDocente(int idDocente)
        {
            using (var dbContext = new ContextDbDataContext())
            {
                Docentes Docen = (from dbD in dbContext.Docentes where dbD.IdDocente == idDocente select dbD).Single();
                Usuario User = (from dbD in dbContext.Usuario where dbD.IdUsuario == Docen.idUsuario select dbD).Single();

                //dbContext.SP_ModificarUsuario(User.IdUsuario, User.contraseña, 0);
                dbContext.SP_ModificaUsuario(User.IdUsuario, User.Usuario1, User.correo, User.contraseña, 0, User.tipo);
            }

            return Json(new { exito = true }, JsonRequestBehavior.AllowGet);
        }


        //******************************CURSO'S METHODS**********************************
        public ActionResult ListaCurso(string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewCurso> ListadoCurso = (from db in db.ViewCurso select db);

            return View(ListadoCurso);
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

        //--------------------------- LISTA USUARIOS ---------------------------
        public ActionResult ListaUsuarios()
        {
            IEnumerable<Usuario> ListaUsuario = (from db in db.Usuario select db);

            return View(ListaUsuario);
        }
    }
}