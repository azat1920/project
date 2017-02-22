using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using System.Data;

namespace project.Controllers
{
    public class EditCookController : Controller
    {

        CookContext db = new CookContext();

        //
        // GET: /Edit/
        //Get form to edit the cook by id
        public ActionResult Edit(int id = 1)
        {   
            Cook cook = db.Cooks.Find(id);
            if (cook == null)
	        {
		        return HttpNotFound();
	        }

            ViewBag.Qualifications = db.Qualifications.ToList();
            ViewBag.Cook = cook;
            ViewBag.Invalide = false;
            return View(cook);
        }

        //Get new cook and add to db
        [HttpPost]
        public ActionResult Edit(Cook cook, int[] qualifications)
        {
            Cook k = db.Cooks.Find(cook.id);
            ViewBag.Qualifications = db.Qualifications.ToList();
            ViewBag.Cook = k;


            ViewBag.Qualifications = db.Qualifications.ToList();
            ViewBag.Invalide = false;   

            if (new Validator().Validate(cook, qualifications))
            {

                var newCook = db.Cooks.Find(cook.id);
                
                newCook.surname = cook.surname;
                newCook.first_name = cook.first_name;
                newCook.patronymic = cook.patronymic;
                newCook.shift_type = cook.shift_type;
                newCook.schedule = cook.schedule;
                newCook.hours = cook.hours;
                
                newCook.qualifications.Clear();
                if (qualifications != null)
                {
                    var t = qualifications.ToList();
                        t.Sort();
                        qualifications = t.ToArray();

                    //получаем выбранные курсы
                    foreach (var c in db.Qualifications.Where(co => qualifications.Contains(co.id)))
                    {
                        newCook.qualifications.Add(c);
                    }
                }


                db.Entry(newCook).State = EntityState.Modified;
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
