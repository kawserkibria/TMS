using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Repository;

namespace Jagoron.Web.Controllers
{
    public class UserInfoController : Controller
    {
        RepTMS objrepository = new RepTMS();
        //
        // GET: /UserInfo/
        public ActionResult UserInfo()
        {
            return View();
        }
        public ActionResult SaveUserInfo(UserInfo obj)
        {
            string oogrp = objrepository.InsertUserInfo(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult UpdateUserInfo(UserInfo obj)
        {
            string oogrp = objrepository.UpdateUserInfo(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowGrid()
        {
            UserInfo obj =new UserInfo();
            List<UserInfo> oogrp = new List<UserInfo>();
            oogrp = objrepository.UserInfoList(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

	}
}