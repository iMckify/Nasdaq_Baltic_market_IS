using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Order
    {
        public int id { get; set; }
        public string isin { get; set; }
        public DateTime request_date { get; set; }
        public DateTime completion_date { get; set; }
        public string fk_Security { get; set; }
        public int fk_Investor { get; set; }
        public int type { get; set; }
    }
}