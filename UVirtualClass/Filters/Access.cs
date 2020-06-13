using UVirtualClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UVirtualClass.Filters
{
    public class Access : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Si Variable de Session is Null, return Login Or Type isn't a Student

            var tipo = HttpContext.Current.Session["Admin"];

            if (tipo == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}