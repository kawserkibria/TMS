using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Repository;

namespace Jagoron.Web.Controllers
{
    public class DressEntryController : Controller
    {
        //
        // GET: /DressEntry/
        RepTMS objrepository = new RepTMS();
        public ActionResult DressEntry()
        {
            return View();
        }
        public ActionResult DressSave(DressSetup obj)
        {

            string oogrp = objrepository.InsertDress(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DressUpdate(DressSetup obj)
        {

            string oogrp = objrepository.UpdateDress(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowGrid()
        {
            List<DressSetup> oogrp = new List<DressSetup>();
            oogrp = objrepository.DressList("0003");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult DressMesurmentInsert(DressSetup obj)
        {

            string oogrp = objrepository.InsertDressMesurment(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DressMesurmentUpdate(DressSetup obj)
        {

            string oogrp = objrepository.UpdateDressMesurment(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DressMesurmentList(DressSetup obj)
        {
            List<DressMesrmentList> oogrp = new List<DressMesrmentList>();
            oogrp = objrepository.DressMesurmentList(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
	}
}