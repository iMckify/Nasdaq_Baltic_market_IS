
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Portfolio
    {
        public int owner_id { get; set; }
        public decimal current_value { get; set; }
        public int fk_Investor { get; set; }
    }
}