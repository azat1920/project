using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;

namespace project.Controllers
{
    public class CooksController : Controller
    {
        //
        // GET: /Cooks/
        CookContext db = new CookContext();

        //Show cooks
        public ActionResult Cooks()
        {
            ViewBag.CountOfCooks = db.Cooks.Count();
            IEnumerable<Cook> cooks = db.Cooks.Include("Qualifications");
            ViewBag.Cooks = cooks;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
