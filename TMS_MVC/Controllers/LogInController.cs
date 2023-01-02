using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jagoron.WEB.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/
        public ActionResult LogIn()
        {
            LogIn login = new LogIn();
            return View("~/Views/Security/LogIn.cshtml");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogIn login)
        {
            
            if (ModelState.IsValid)
            {
                string UserID = login.username;
                Session["USERID"] = login.username;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
           
        }
	}
}