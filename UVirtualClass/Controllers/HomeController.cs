using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UVirtualClass.DataContext;
using UVirtualClass.Models;
using System.Threading.Tasks;

namespace UVirtualClass.Controllers
{
    public class HomeController : Controller
    {
        SessionData session = new SessionData();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Login(Login datos)
        {

            if (ModelState.IsValid)
            {
                if (datos.Validacion())
                {
                    session.setSession("Usuario", datos.Usuario);
                    session.setSession("idUsuario", datos.IdUsuario.ToString());
                    session.setSession("TipoUsuario", datos.tipo);
                    if (datos.tipo == "1")
                    {
                        session.setSession("Admin", datos.tipo);
                    }
                }
            }

            return RedirectToAction("Index", "AdminHome");
        }

        public ActionResult Close()
        {
            session.destroySession();
            return RedirectToAction("Index", "Home");
        }
    }
}