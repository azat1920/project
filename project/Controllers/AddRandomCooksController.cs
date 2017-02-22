using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;
using System.Threading;

namespace project.Controllers
{
    public class AddRandomCooksController : Controller
    {
        //
        // GET: /AddRandomCooks/


        CookContext db = new CookContext();
        Random random = new Random();
        Cook newCook;



        //Add random cooks, if they dont exist
        public ActionResult Index()
        {
            if (db.Cooks.Count() > 0)
            {
                return RedirectToAction("Cooks", "Cooks");
            }

            List<string> surname = new List<string> { "Иванов", "Петров", "Сидоров", "Козлов", "Котов", "Удалов", "Жданов", "Жилонов"};

            List<string> firstName = new List<string> { "Иван", "Пётр", "Никита", "Андрей", "Антон", "Александр", "Алексей", "Василий" };

            List<string> patronymic = new List<string> { "Александрович", "Алексеевич", "Андреевич", "Антонович" };

            List<string> shift_type = new List<string> { "утренняя", "вечерняя" };

            List<string> schedule = new List<string> { "5/2", "2/2" };

            List<Qualification> list;
            

            for (int i = 0; i < 100; i++)
            {

                
                newCook = new Cook();
                newCook.surname = surname.ElementAt(random.Next(surname.Count));
                newCook.first_name = firstName.ElementAt(random.Next(firstName.Count));  
                newCook.patronymic = patronymic.ElementAt(random.Next(patronymic.Count));
                newCook.shift_type = shift_type.ElementAt(random.Next(shift_type.Count));
                newCook.schedule = schedule.ElementAt(random.Next(schedule.Count));  
                newCook.hours = random.Next(4,11); 


                list = new List<Qualification>();

                List<Qualification> qualifications = db.Qualifications.ToList();

                foreach (var item in qualifications)
                {
                    int rand = random.Next(0, 2);
                    if (rand != 0)
                    {
                        list.Add(item);
                    }
                }

                if (list.Count == 0)
                {
                    int j = random.Next(0, 3);
                    list.Add(db.Qualifications.ToList().ElementAt(j));
                }

                newCook.qualifications = list;
                db.Cooks.Add(newCook);
             
            }

            

            db.SaveChanges();
            return RedirectToAction("Cooks", "Cooks");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }


}
