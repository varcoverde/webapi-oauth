using Newtonsoft.Json;
using SiteAuth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SiteAuth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Index()
        {

            var cp = (ClaimsPrincipal)User;
            var email = cp.FindFirst(ClaimTypes.Email);


            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid) //Required, string length etc
            {
                //var userStore = new UserRepository();
                //var user = userStore.FindUser(model.UserName, model.Password);
                //if (user != null)
                if (model.UserName == "victor" && model.Password == "1234")
                {
                    User user = new Models.User
                    {
                        Id = 1,
                        Name = "Victor",
                        Email = "victor@email.com",
                        Roles = new List<string>() { "User", "Admin" }
                    };

                    //FormsAuthentication.SetAuthCookie(user.Name, false);
                    SetAuthCookie(user);

                    SetClaimsCookie(user);


                    //redirect to returnUrl
                    if (!string.IsNullOrEmpty(returnUrl) &&
                        Url.IsLocalUrl(returnUrl) &&
                        !returnUrl.Equals("/Error/NotFound", StringComparison.OrdinalIgnoreCase))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("~/");
                }
                ModelState.AddModelError("UserName", "User or password not found");
            }
            return View(model);
        }

        private void SetAuthCookie(User user)
        {
            var userData = JsonConvert.SerializeObject(user);
            var authTicket = new FormsAuthenticationTicket(
                  1, //version
                  user.Name,
                  DateTime.Now, //issue date
                  DateTime.Now.AddMinutes(30), //expiration
                  false,  //isPersistent
                  userData,
                  FormsAuthentication.FormsCookiePath); //cookie path
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                        FormsAuthentication.Encrypt(authTicket));
            
            Response.Cookies.Add(cookie);
        }


        private void SetClaimsCookie(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //needs an authentication issuer otherwise not authenticated
            var claimsIdentity = new ClaimsIdentity(claims, "Forms");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var sessionAuthenticationModule = FederatedAuthentication.SessionAuthenticationModule;
            var token = new SessionSecurityToken(claimsPrincipal);
            sessionAuthenticationModule.WriteSessionTokenToCookie(token);
        }

        public ActionResult SignOut()
        {
            var sessionAuthenticationModule = FederatedAuthentication.SessionAuthenticationModule;
            sessionAuthenticationModule.CookieHandler.Delete();

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            
            //FormsAuthentication.SignOut();
            return Redirect("Index");
        }
    }
}