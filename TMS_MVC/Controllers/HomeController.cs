
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jagoron.Web.Controllers
{
    public class HomeController : Controller
    {
        //Respatatory _objConn_ = new Respatatory();
        public ActionResult Index()
        {
            //var notMessageList = _objConn_.gstrGetNotificationMessgae().ToList();
            //var notifi = _objConn_.gstrGetNotification().FirstOrDefault();
            //notifi.strNotiMessage = new List<string>() { "jhjhjhjh","rahim"};
            //notifi.NotiMessage = notMessageList;
            //notifi.NotiMessage = notMessageList;
            //notifi.lngNorthOrder = 300;
            //notifi.lngWestOdrer = 120;
            //ViewBag.Notify = notifi;
            return View();
           
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}