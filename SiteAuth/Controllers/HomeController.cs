using RestSharp;
using SiteAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SiteAuth.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            var client = new RestClient("http://api.localoauth.com.br");

            var request = new RestRequest("api/security/token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", Username);
            request.AddParameter("password", Password);

            IRestResponse<TokenViewModel> response = client.Execute<TokenViewModel>(request);
            var token = response.Data.access_token;

            if (!String.IsNullOrEmpty(token))
                FormsAuthentication.SetAuthCookie(token, false);

            

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}