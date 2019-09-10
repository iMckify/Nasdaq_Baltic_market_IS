using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace NASDAQ.ViewModels
{
    public class PriceConsolidatedViewModel
    {
        [DisplayName("Company")]
        public string company { get; set; }
        [DisplayName("Ticker")]
        public string fk_Security { get; set; }
        [DisplayName("Price")]
        public decimal value { get; set; }
        [DisplayName("Sold count")]
        public int count { get; set; }
        [DisplayName("Growth (%)")]
        public decimal sum { get; set; }
    }
}