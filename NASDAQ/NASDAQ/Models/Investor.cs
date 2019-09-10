using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Investor
    {
        public int person_code { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public decimal invested_amount { get; set; }
        public string email { get; set; }
    }
}