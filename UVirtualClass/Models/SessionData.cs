using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVirtualClass.Models
{
    public class SessionData
    {
        private String session;

        public String getSession(String name)
        {
            this.session = Convert.ToString(HttpContext.Current.Session[name]);

            return session;
        }

        public void setSession(String name, object data)
        {
            HttpContext.Current.Session[name] = data;
        }

        public void destroySession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();
        }
    }
}