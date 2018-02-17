using Newtonsoft.Json;
using SiteAuth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SiteAuth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        /*
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return;
            }

            var formsIdentity = context.User.Identity as FormsIdentity;
            if (formsIdentity == null)
            {
                return;
            }

            var id = formsIdentity;
            var ticket = id.Ticket;
            var userData = ticket.UserData; // Get the stored user-data, in this case, our roles
            var user = JsonConvert.DeserializeObject<User>(userData);
            var customPrincipal = new UserPrincipal(formsIdentity, user.Roles.ToArray());
            customPrincipal.Email = user.Email;
            Thread.CurrentPrincipal = Context.User = customPrincipal;
        }
        */
    }
}
