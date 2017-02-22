using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class Day
    {
        public int id { get; set; }
        

        public List<RestaurantDay> list { get; set; }

        public Day(int rest_count) 
        {
            list = new List<RestaurantDay>();
            for (int i = 0; i < rest_count; i++)
            {
                list.Add(new RestaurantDay());
            }
        }

   
    }
}