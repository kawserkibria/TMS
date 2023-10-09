using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Repository;

namespace Jagoron.Web.Controllers
{
  
    public class DirectorInformationController : Controller
    {
        //
        // GET: /DirectorInformation/
        RepTMS objrepository = new RepTMS();
        public ActionResult DirectorInformation()
        {
            return View();
        }
        public ActionResult mPorichalokSave(Porichalok obj)
        {

            string oogrp = objrepository.mInsertPorichalok(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mPorichalokUpdate(Porichalok obj)
        {
   
            string oogrp = objrepository.mUpdatePorichalok(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult mFillEdit(Porichalok obj)
        {
            Porichalok oogrp = new Porichalok();
            oogrp = objrepository.mFillShowGridPorichalok("0003");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DeleteButton(ButtonName obj)
        {
            var oogrp = objrepository.DeleteButton(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
	}
}