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
        RepTMS objrepository = new RepTMS();
        //
        // GET: /Company/
         //[Route("Index")]
        public ActionResult CreateCompany()
        {
          
            return View("~/Views/TMS/CreateCompany.cshtml");
        }

        public ActionResult mCompanaySave(ProthistanModel obj)
        {
            string oogrp = objrepository.mInsertProthistan(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };

        }

        //public ActionResult mCompanayUpdate(ProthistanModel obj)
        //{
        //    var byts = new byte[0];
        //    if ((objCompany.Picture != null) && (objCompany.Picture.ContentLength > 0))
        //    {

        //        var pic = objCompany.Picture.InputStream;

        //        MemoryStream ms = new MemoryStream();
        //        pic.CopyTo(ms);
        //        byts = ms.ToArray();
        //        ms.Dispose();
        //    }

        //    string oogrp = objrepository.mUpdateEmployeeImage("0001", objCompany.CompanayName, objCompany.MobileNumber, objCompany.inStatus, byts, objCompany.Id);
        //    return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}



        //[HttpPost]
        //public ActionResult Index(ProthistanModel objCompany)
        //{
        //    List<ProthistanModel> images = objrepository.GetImages();
        //    ProthistanModel image = images.Find(p => p.strPROTHISTAN_ID == objCompany.strPROTHISTAN_ID);
        //    if (image != null)
        //    {
        //        image.IsSelected = true;
        //        ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.strIamge, 0, image.strIamge.Length);
        //    }
        //    return View(images);
        //}

        //public ActionResult mFillShowGrid()
        //{
        //    List<CompanyEntry> oogrp = new List<CompanyEntry>();
        //    oogrp = repjitorytms.mFillShowGrid("0003");
        //    return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}

        //public ActionResult DeleteCompany(CompanyEntry obj)
        //{
        //    var oogrp = repjitorytms.DeleteCompany(obj);
        //    return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        //}
   

	}
}