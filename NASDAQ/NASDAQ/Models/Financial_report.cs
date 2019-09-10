using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace NASDAQ.Models
{
    public class Financial_report
    {
        [DisplayName("Ticker")]
        public string ticker { get; set; }
        [DisplayName("Reporting Period")]
        public string reporting_period { get; set; }
        [DisplayName("Statement type")]
        public string statement_type { get; set; }
        [DisplayName("Release date")]
        public DateTime release_date { get; set; }
        [DisplayName("P/E")]
        public decimal P_E { get; set; }
        [DisplayName("P/B")]
        public decimal P_B { get; set; }
        [DisplayName("EV/EBITDA")]
        public decimal EV_EBITDA { get; set; }
        [DisplayName("NetDepth/EBITDA")]
        public decimal NetDepth_EBITDA { get; set; }
        [DisplayName("ROA")]
        public decimal ROA { get; set; }
        [DisplayName("Company")]
        public int fk_Company { get; set; }
    }
}