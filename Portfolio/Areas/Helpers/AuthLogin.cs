using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfolio.Models;

namespace Portfolio.Areas.Helpers
{
        public class AuthLogin : ActionFilterAttribute
        {
            private readonly PorfolioEntities db;
            public AuthLogin()
            {
                db = new PorfolioEntities();
            }


            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);

                if (HttpContext.Current.Request.Cookies["cookie"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Login" },
                    { "action", "Index" }
                   });
                    return;
                }

                string token = HttpContext.Current.Request.Cookies["cookie"].Value.ToString();
                Admin user = db.Admins.FirstOrDefault(u => u.Token == token);

                if (user == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Login" },
                    { "action", "Index" }
                   });
                }


            }
        }
}