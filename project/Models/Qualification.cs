using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class Qualification
    {
        public int id { get; set; }
        public string qualification { get; set; }

        public virtual ICollection<Cook> cooks { get; set; }

        public Qualification()
        {
            cooks = new List<Cook>();
        }
    }
}