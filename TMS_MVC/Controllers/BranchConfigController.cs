//using Jagoron.Web.Controllers.DAL;
//using Jagoron.Web.Models.Accounts;
//using Jagoron.Web.Repository.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Models;

namespace Jagoron.Web.Controllers.Accounts
{
    public class BranchConfigController : Controller
    {

        //ACCMS _objConn_ = new ACCMS();
        // GET: /BranchConfig/
        public ActionResult BranchConfigList()
        {
            return View("~/Views/TMS/BranchConfigList.cshtml");
        }

        public ActionResult mEMPSave(ProthistanModel objEmployee)
        {
            var byts = new byte[0];
            if ((objEmployee.Picture != null) && (objEmployee.Picture.ContentLength > 0))
            {
                var pic = objEmployee.Picture.InputStream;
                MemoryStream ms = new MemoryStream();
                pic.CopyTo(ms);
                byts = ms.ToArray();
                ms.Dispose();
            }

            //string oogrp = _leaveAppService.mInsertEmployeeImage("0001", "", byts);
            return new JsonResult() { Data = "", JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        //public ActionResult mFillShowGrid()
        //{
        //    List<BranchConfig> oogrp = new List<BranchConfig>();
        //    oogrp = _objConn_.mFillShowGrid("0003");
        //    return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}
      

        //public ActionResult DeleteBranchInfo(BranchConfig reqObj)
        //{
        //    string strSave = "save";
        //    strSave = _objConn_.mDeleteBranchInfo("0003", reqObj);
        //    return new JsonResult() { Data = strSave, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}
        //public ActionResult mUpdateBranchInfo(BranchConfig reqObj)
        //{
        //    string strSave = "Update";
        //    strSave = _objConn_.mUpdateBranchInfo("0003", reqObj);
        //    return new JsonResult() { Data = strSave, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}


        //public ActionResult SaveBranchInfo(BranchConfig reqObj)
        //{
        //    string strSave = "save";
        //    strSave = _objConn_.mSaveBranchInfo("0003", reqObj);
        //    return new JsonResult() { Data = strSave, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}
        
        
	}
}