using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoversController : Controller
    {
        private ApplicationDbContext _context;
        
        public MoversController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult NewMover()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Mover mover)
        {
            if (!ModelState.IsValid)
            {
                return View("NewMover");
            }

            mover.PostedOn = DateTime.Now;
            _context.Movers.Add(mover);
            _context.SaveChanges();

            TempData["success"] = "Mover added successfully !";

            return RedirectToAction("NewMover");
        }
        // GET: Movers
        public ActionResult Index()
        {
            return View();
        }
    }
}