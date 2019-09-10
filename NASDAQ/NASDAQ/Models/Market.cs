using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Market
    {
        public string name { get; set; }
        public string country { get; set; }
        public int id { get; set; }
        public int fk_Order { get; set; }
        public int fk_Company { get; set; }
    }
}