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
        public ActionResult mButtonUpdate(ButtonName obj)
        {
            var byts = new byte[0];
            if ((obj.Picture != null) && (obj.Picture.ContentLength > 0))
            {

                var pic = obj.Picture.InputStream;

                MemoryStream ms = new MemoryStream();
                pic.CopyTo(ms);
                byts = ms.ToArray();
                ms.Dispose();
            }

            string oogrp = objrepository.mUpdateButton(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult mFillShowGrid()
        {
            List<ButtonName> oogrp = new List<ButtonName>();
            oogrp = objrepository.mFillShowGridBrand("0003");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult DeleteButton(ButtonName obj)
        {
            var oogrp = objrepository.DeleteButton(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
	}
}