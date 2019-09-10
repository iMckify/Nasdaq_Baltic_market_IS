
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Portfolio_security
    {
        public int count { get; set; }
        public int id { get; set; }
        public int fk_Portfolio { get; set; }
        public string fk_Security { get; set; }
    }
}