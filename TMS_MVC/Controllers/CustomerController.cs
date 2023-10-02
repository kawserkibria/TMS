using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jagoron.Web.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Customerview()
        {
            ViewBag.TotalStudents = 5;
            return View();
        }
	}
}