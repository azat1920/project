using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;


namespace project.Controllers
{
    public class ScheduleController : Controller
    {
        CookContext db = new CookContext();
        
        //
        // GET: /Schedule/
        
        public ActionResult Index(int id = 0)
        
        {
            int days_count = 31;
            ViewBag.num = id + 1;
            ScheduleGenerator sg = new ScheduleGenerator();
            ViewBag.schedule = sg.GenerateSchedule(db.Cooks.ToList(), db.Qualifications.ToList(), days_count, id);
            ViewBag.days_count = days_count;
            return View();
            
        }


    }
}
