using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNotification()
        {
            return Json(NotificationService.GetNotification(), JsonRequestBehavior.AllowGet);
        }
    }
}