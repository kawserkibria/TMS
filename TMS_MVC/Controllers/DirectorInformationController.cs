using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jagoron.Web.Controllers
{
  
    public class DirectorInformationController : Controller
    {
        //
        // GET: /DirectorInformation/
     
        public ActionResult DirectorInformation()
        {
            return View();
        }
        public ActionResult mPorichalokSave(Porichalok obj)
        {

            //string oogrp = objrepository.mInsertButton(obj);
            return new JsonResult() { Data = "", JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
	}
}