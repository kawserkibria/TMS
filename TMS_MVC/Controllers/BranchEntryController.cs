using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Repository;

namespace Jagoron.Web.Controllers
{
    public class BranchEntryController : Controller
    {
        RepTMS objrepository = new RepTMS();
        //
        // GET: /BranchEntry/
        // GET: /BranchEntry/
        public ActionResult BranchEntry()
        {
            return View("~/Views/TMS/BranchEntry.cshtml");
        }
        public ActionResult mBranchSave(BranchEntry obj)
        {

            string oogrp = objrepository.mInsertBranch(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowBranchName(BranchEntry obj)
        {
            List<BranchEntry> oogrp = new List<BranchEntry>();
            oogrp = objrepository.mFillShowBranchName(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mUpdateBranchInfo(BranchEntry obj)
        {
            obj.intCompanayID =0;
            string strSave = "Update";
            strSave = objrepository.mUpdateBranch(obj);
            return new JsonResult() { Data = strSave, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult DeleteBranch(BranchEntry obj)
        {
            obj.intCompanayID = 0;
            var oogrp = objrepository.DeleteBranch(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

	}
}