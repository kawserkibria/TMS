using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TMS.Models;
using TMS.Repository;

namespace TMS.Controllers
{

    public class CompanyController : Controller
    {
        string ConnectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
        RepTMS repjitorytms = new RepTMS();
        //
        // GET: /Company/
         //[Route("Index")]
        public ActionResult CreateCompany()
        {
          
            return View("~/Views/TMS/CreateCompany.cshtml");
        }

        public ActionResult mCompanaySave(CompanyEntry objCompany)
        {
            var byts = new byte[0];
            if ((objCompany.Picture != null) && (objCompany.Picture.ContentLength > 0))
            {

                var pic = objCompany.Picture.InputStream;

                MemoryStream ms = new MemoryStream();
                pic.CopyTo(ms);
                byts = ms.ToArray();
                ms.Dispose();
            }

            string oogrp = repjitorytms.mInsertEmployeeImage("0001", objCompany.CompanayName, objCompany.MobileNumber, objCompany.inStatus, byts);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult mCompanayUpdate(CompanyEntry objCompany)
        {
            var byts = new byte[0];
            if ((objCompany.Picture != null) && (objCompany.Picture.ContentLength > 0))
            {

                var pic = objCompany.Picture.InputStream;

                MemoryStream ms = new MemoryStream();
                pic.CopyTo(ms);
                byts = ms.ToArray();
                ms.Dispose();
            }

             string oogrp = repjitorytms.mUpdateEmployeeImage("0001", objCompany.CompanayName, objCompany.MobileNumber, objCompany.inStatus, byts, objCompany.Id);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }



        [HttpPost]
        public ActionResult Index(CompanyEntry objCompany)
        {
            List<CompanyEntry> images = repjitorytms.GetImages();
            CompanyEntry image = images.Find(p => p.Id == objCompany.Id);
            if (image != null)
            {
                image.IsSelected = true;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.strIamge, 0, image.strIamge.Length);
            }
            return View(images);
        }

        public ActionResult mFillShowGrid()
        {
            List<CompanyEntry> oogrp = new List<CompanyEntry>();
            oogrp = repjitorytms.mFillShowGrid("0003");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult DeleteCompany(CompanyEntry obj)
        {
            var oogrp = repjitorytms.DeleteCompany(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
   

	}
}