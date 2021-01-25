using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Parkovka.Models
{
    public class Sahifa
    {
        public List<Xodim> Xodim { get; set; }
        public List<Narx> Narx { get; set; }
        public List<Joylar> Joylar { get; set; }
        public List<JadvalFoy> JadvalFoy { get; set; }
        public List<JadvalXodim> JadvalXodim { get; set; }




    }
}