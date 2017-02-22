using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using System.Data;

namespace project.Controllers
{
    public class DeleteCookController : Controller
    {
        //
        // GET: /DeleteCook/
        CookContext db = new CookContext();


        //Delete cook by id
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            var t = db.Cooks.Find(id);
            if (id != 0 && t != null)
            {
                db.Cooks.Remove(t);
                db.Entry(t).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return RedirectToAction("Cooks", "Cooks");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
