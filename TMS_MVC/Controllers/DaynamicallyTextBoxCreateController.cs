using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Repository;

namespace Jagoron.Web.Controllers
{
    public class DaynamicallyTextBoxCreateController : Controller
    {
        RepTMS objrepository = new RepTMS();
        //
        // GET: /DaynamicallyTextBoxCreate/
        public ActionResult DaynamicallyTextBoxCreateView(int intCustomer, string strOrderNO)
        {
            ViewBag.custcode = intCustomer;
            ViewBag.strOrderNO = strOrderNO;
            return View("~/Views/TMS/DaynamicallyTextBoxCreate.cshtml");
        }

        public ActionResult DaynamicallyTextBoxCreateViewEdit(int intCustomer, string strOrderNO)
        {
            ViewBag.custcode = intCustomer;
            ViewBag.strOrderNO = strOrderNO;
            return View("~/Views/TMS/DaynamicallyTextBoxCreate.cshtml");
        }
        public ActionResult OrderListView( )
        {
            return View("~/Views/TMS/OrderList.cshtml");
        }
        public ActionResult GetCategoryList()
        {
            List<DressSetup> oogrp = new List<DressSetup>();
            oogrp = objrepository.mFillMeserment("0003");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult getDataList(int Id, int intAddMode, string strOrderNo)
        {
            List<CategoryViewModel> oogrp = new List<CategoryViewModel>();
            if (intAddMode == 0)
            {
                oogrp = objrepository.mFillDressMesermen(Id, 0, strOrderNo);
            }
            else
            {
                oogrp = objrepository.mFillDressMesermen(Id, 1, strOrderNo);
            }
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }


        public ActionResult getTextVal(int intid, string strOrderNo, int intEditMode)
        {
            List<OrderDList> oogrp = new List<OrderDList>();
            oogrp = objrepository.mGetTextVal(intid, strOrderNo);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

       
        public JsonResult OrderSave(OrderM objStockItem)
        {


            string strmassage = "";
            strmassage = objrepository.mInsertOrder(objStockItem);
            return new JsonResult() { Data = strmassage, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult OrderList()
        {
            List<OrderList> oogrp = new List<OrderList>();
            oogrp = objrepository.OrderList();
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
       
	}
}