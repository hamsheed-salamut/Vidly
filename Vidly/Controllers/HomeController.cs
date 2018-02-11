using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Hubs;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public JsonResult GetNotificationContacts()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;

            NotificationComponents nc = new NotificationComponents();

            var list = nc.GetMovers(notificationRegisterTime);

            Session["LastUpdate"] = DateTime.Now;

            return new JsonResult
            {
                Data = list,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}