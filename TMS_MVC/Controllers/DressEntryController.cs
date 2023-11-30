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


        public ActionResult SelectCompanyList(DatabaseCompany obj)
        {
            List<DatabaseCompany> oogrp = new List<DatabaseCompany>();
            oogrp = objrepository.mloadDatabaseCompnay("0004");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult SelectCompany()
        {
            return View();
        }
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
        public ActionResult DressStyle(DressSetup obj)
        {
            List<DressMesrmentList> oogrp = new List<DressMesrmentList>();
            oogrp = objrepository.DressMesurmentList(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DressStyleInsert(DressStyleList obj)
        {

            string oogrp = objrepository.InsertDressStyle(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DressStyleUpdate(DressStyleList obj)
        {

            string oogrp = objrepository.UpdateDressStyle(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowDressStyle(int intDressid)
        {
            List<DressStyleList> oogrp = new List<DressStyleList>();
            oogrp = objrepository.StyleList(intDressid);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DeleteDressStyle(DressStyleList obj)
        {

            string oogrp = objrepository.DeleteDressStyle(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult DressStyleDetailsInsert(DressStyleList obj)
        {



            string oogrp = objrepository.InsertDressStyleDetails(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        #region "Cookis data Add and Get"
        public ActionResult mFillShowDressStyle1(int intDressid)
        {
            string key="1" , value="One";
            int expireDay = 1;
            var cookie = new HttpCookie(key, value);
            cookie.Expires = DateTime.Now.AddDays(expireDay);
            HttpContext.Response.Cookies.Add(cookie);


            return new JsonResult() { Data = cookie, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowDressStyle3(int intDressid)
        {
            string value = string.Empty;

            var cookie = Request.Cookies["1"];

            if (cookie != null)
            {
                if (string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return new JsonResult() { Data = value, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
                }
                value = cookie.Value;
            }

            return new JsonResult() { Data = cookie, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        #endregion


    }
}