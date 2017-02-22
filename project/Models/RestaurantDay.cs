using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class RestaurantDay
    {
        public int RestaurantId { get; set; }
        public List<Cook> cooks { get; set; }
        public int count { get; set; }
        public int count_2 { get; set; }
        public int count_3 { get; set; }
        public int count_rus { get; set; }
        public int count_jap { get; set; }
        public int count_ital { get; set; }

        public RestaurantDay()
        {
            cooks = new List<Cook>();
            count_2 = new int();
        }

        public RestaurantDay(RestaurantDay day)
        {
            
            this.count = day.count;
            this.count_2 = day.count_2;
            this.count_3 = day.count_3;
            this.cooks = new List<Cook>();
            if (day.cooks.Count > 0)
            {
                for (int i = 0; i < day.cooks.Count; i++)
                {
                    var cook = day.cooks[i];
                    this.cooks.Add(cook);
                }

            }

        }


    }
}