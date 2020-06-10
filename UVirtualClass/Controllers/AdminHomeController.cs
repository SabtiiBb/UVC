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
        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        //******************************ALUMNO'S METHODS**********************************
        public ActionResult ListaAlumnos(string a)
        {
            @ViewBag.Accion = a;
            IEnumerable<ViewAlumnos> ListadoAlumno;

            using (var dbContext = new ContextDbDataContext())
            {
                ListadoAlumno = (from dbD in dbContext.ViewAlumnos select dbD).ToList();
            }

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
                    dbContext.SP_ModificaUsuario(User.IdUsuario, User.Usuario1, User.correo, User.contraseña, User.Activo, User.tipo);
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

        public ActionResult ListaDocente(string a)
        {
            @ViewBag.Accion = a;

            IEnumerable<ViewDocentes> ListadoDocentes;
            using (var dbContext = new ContextDbDataContext())
            {
                ListadoDocentes = (from db in dbContext.ViewDocentes select db).ToList();
            }

            return View(ListadoDocentes);
        }

        // "CrearDocente", "AdminHome", FormMethod.Post, new { @class = "crearalumno" }
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
                    User.correo = MyModel.correo;
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
                    dbContext.SP_ModificaUsuario(User.IdUsuario, User.Usuario1, User.correo, User.contraseña, User.Activo, User.tipo);
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

            IEnumerable<ViewCurso> ListadoCurso;
            using (var dbContext = new ContextDbDataContext())
            {
                ListadoCurso = (from db in dbContext.ViewCurso select db).ToList();
            }

            return View(ListadoCurso);
        }

        [HttpGet]
        public ActionResult CrearCurso()
        {
            List<Docentes> docentes;

            List<SelectListItem> lst = new List<SelectListItem>();

            using (var dbContext = new ContextDbDataContext())
            {
                docentes = (from db in dbContext.Docentes select db).ToList();
                foreach (var item in docentes)
                {
                    lst.Add(new SelectListItem() { Text = item.nombre, Value = item.IdDocente.ToString() });
                }
            }
                ViewBag.docente = lst;

            return View();
        }

        [HttpPost]
        public ActionResult CrearCurso(FormCollection c, CreaCurso MyModel)
        {
            using (var dbContext = new ContextDbDataContext())
            {

                Cursos Curso = new Cursos();

                Curso.Nombre = MyModel.Nombre;
                Curso.Descripcion = MyModel.Descripcion;
                Curso.Recursos = MyModel.Recursos;
                Curso.Costo = MyModel.Costo;
                Curso.idDocente = Convert.ToInt32(MyModel.idDocente);
                Curso.Foto = MyModel.Foto;
                Curso.Videointro = MyModel.VideoIntro;
                dbContext.Cursos.InsertOnSubmit(Curso);
                dbContext.SubmitChanges();

                var List = (from dbD in dbContext.Cursos select dbD).ToList();
                Curso = List.LastOrDefault();
                MyModel.idCurso = Curso.IdCurso;

            }
            var modelito = MyModel;
            return RedirectToAction("CrearTemario", "AdminHome", modelito);
        }

        [HttpGet]
        public ActionResult CrearTemario(CreaCurso Model)
        {
            return View(Model);
        }

        [HttpPost]
        public ActionResult CrearTemario(FormCollection e, CreaCurso MyModel)
        {
            using (var dbContext = new ContextDbDataContext())
            {
                Temario TempTemp = new Temario();
                TempTemp.IdCurso = MyModel.idCurso;
                TempTemp.Tema = MyModel.NombreTema;
                TempTemp.Descripcion = MyModel.DescripcionTema;
                TempTemp.FotoTema = MyModel.VideoTema;

                dbContext.Temario.InsertOnSubmit(TempTemp);
                dbContext.SubmitChanges();

                MyModel.NombreTema = "";
                MyModel.DescripcionTema = "";
                MyModel.VideoTema = "";
            }
            return RedirectToAction("CrearTemario", "AdminHome", MyModel);
        }

        [HttpGet]
        public ActionResult EditarCurso(int idCurso)
        {
            EditarCursoVM MyModel = new EditarCursoVM();
            using (var dbContext = new ContextDbDataContext())
            {
                Cursos Curs = (from dbD in dbContext.Cursos where dbD.IdCurso == idCurso select dbD).Single();
                MyModel.IdCurso = idCurso;
                MyModel.Nombre = Curs.Nombre;
                MyModel.Descripcion = Curs.Descripcion;
                MyModel.Costo = Convert.ToDecimal(Curs.Costo);
                MyModel.idDocente = Convert.ToInt32(Curs.idDocente);
                MyModel.Recursos = Curs.Recursos;
                MyModel.Foto = Curs.Foto;
                MyModel.VideoIntro = Curs.Videointro;
            }
            return View(MyModel);
        }

        [HttpPost]
        public ActionResult EditarCurso(EditarCursoVM MyModel)
        {
            using (var dbContext = new ContextDbDataContext())
            {
                Cursos Curs = (from dbD in dbContext.Cursos where dbD.IdCurso == MyModel.IdCurso select dbD).Single();
                Curs.Nombre = MyModel.Nombre;
                Curs.Descripcion = MyModel.Descripcion;
                Curs.Costo = Convert.ToDecimal(MyModel.Costo);
                Curs.idDocente = Convert.ToInt32(MyModel.idDocente);
                Curs.Recursos = MyModel.Recursos;
                Curs.Foto = MyModel.Foto;
                dbContext.SP_ModificaCursos(Curs.IdCurso, Curs.Nombre, Curs.Descripcion, Curs.Recursos, Curs.Costo, Curs.Foto);
                dbContext.SubmitChanges();
            }
            return View(MyModel);
        }



        //******************************PAGO'S METHODS**********************************

        [HttpGet]
        public ActionResult ListadoPago()
        {
            List<Pagos> ListadoPagos;
            using (var dbContext = new ContextDbDataContext())
            {
                ListadoPagos = (from db in dbContext.Pagos select db).ToList();
            }
            return View(ListadoPagos);
        }

        //--------------------------- VALIDATIONS CLIENT'S SIDE ---------------------------
        [HttpPost]
        public JsonResult UserRequestValidation(string Usuario1)
        {
            return Json(UsuarioRegistrado(Usuario1));
        }



        public bool UsuarioRegistrado(string User)
        {
            bool ifExist;

            using (var dbContext = new ContextDbDataContext())
            {
                var UsuRegis = (from db in dbContext.Usuario
                                where db.Usuario1.ToUpper() == User.ToUpper()
                                select new { User }).FirstOrDefault();

                ifExist = UsuRegis != null ? false : true;
            }

            return ifExist;
        }



        [HttpPost]
        public JsonResult EmailRequestValidation(string correo)
        {
            return Json(CorreoRegistrado(correo));
        }
        public bool CorreoRegistrado(string Email)
        {

            bool IfExist;

            using (var dbContext = new ContextDbDataContext())
            {
                var EmailExist = (from db in dbContext.Usuario
                                  where db.correo.ToUpper() == Email.ToUpper()
                                  select new { Email }).FirstOrDefault();

                IfExist = EmailExist != null ? false : true;
            }

            return IfExist;
        }

        //--------------------------- LISTA USUARIOS ---------------------------
        public ActionResult ListaUsuarios()
        {
            IEnumerable<Usuario> ListaUsuario;

            using (var dbContext = new ContextDbDataContext())
            {
                ListaUsuario = (from dbD in dbContext.Usuario select dbD).ToList();
            }

                return View(ListaUsuario);
        }
    }
}