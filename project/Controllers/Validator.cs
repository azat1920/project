using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project.Models;

namespace project.Controllers
{
    public class Validator
    {

        public bool Validate(Cook cook, int[] q)
        {
            if (q == null || q.Length < 1)
            {
                return false;
            }

            var t = cook.shift_type;
            if (t == null || !(t.Equals("утренняя") || t.Equals("вечерняя")))
            {
                return false;
            }

            t = cook.schedule;

            if (t == null || !(t.Equals("5/2") || t.Equals("2/2")))
            {
                return false;
            }

            t = cook.first_name;
            if (t == null)
            {
                return false;
            }

            t = cook.surname;
            if (t == null)
            {
                return false;
            }

            if (cook.hours == null || !(cook.hours >= 4 && cook.hours <= 10))
            {
                return false;
            }


            return true;
        }
    }


}