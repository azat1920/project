using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using System.Data.Entity;

namespace project.Controllers
{
    public class CreateCookController : Controller
    {
        CookContext db = new CookContext();

        //
        // GET: /CreateCook/
        
        //Show form for creating a cook
        public ActionResult Create()
        {
            ViewBag.Qualifications = db.Qualifications.ToList();
            ViewBag.Invalide = false;
            return View();
        }


        //Add the cook to db
        [HttpPost]
        public ActionResult Create(Cook cook, int[] qualifications)
        {

            ViewBag.Qualifications = db.Qualifications.ToList();
            ViewBag.Invalide = false;

            if (new Validator().Validate(cook, qualifications))
            {

                Cook newCook = new Cook();
                newCook.surname = cook.surname;
                newCook.first_name = cook.first_name;
                newCook.patronymic = cook.patronymic;
                newCook.shift_type = cook.shift_type;
                newCook.schedule = cook.schedule;
                newCook.hours = cook.hours;


                foreach (var item in qualifications)
                {
                    newCook.qualifications.Add(db.Qualifications.Find(item));
                }

                db.Cooks.Add(newCook);
                db.SaveChanges();

                return RedirectToAction("Cooks", "Cooks");

            }
            else
            {
                ViewBag.Invalide = true;
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
