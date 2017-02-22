using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class Cook
    {
        public int id { get; set; }
        public string surname { get; set; }
        public string first_name { get; set; }
        public string patronymic { get; set; }
        //type of shift 'утренняя' or 'вечерняя'
        public string shift_type { get; set; }
        //shedule - 5/2 or 2/2 
        public string schedule { get; set; }
        //count of hours 4..10
        public int hours { get; set; }

        public virtual ICollection<Qualification> qualifications { get; set; }

        public Cook()
        {
            qualifications = new List<Qualification>();
        }

    }
}