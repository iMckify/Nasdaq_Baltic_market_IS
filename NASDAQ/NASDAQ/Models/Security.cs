using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NASDAQ.Models
{
    public class Security
    {
        public string isin { get; set; }
        public string ticker { get; set; }
        public decimal recent_volatility { get; set; }
        public string list_segment { get; set; }
        public string issuer { get; set; }
        public decimal nominal_value { get; set; }
        public int total_number_of_securities { get; set; }
        public int listed_securities_number { get; set; }
        public DateTime listing_date { get; set; }
        public int fk_Company { get; set; }
    }
}