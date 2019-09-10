using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace NASDAQ.ViewModels
{
    public class SecurityEditViewModel
    {
        [DisplayName("Isin")]
        [Required]
        public string isin { get; set; }
        [DisplayName("Ticker")]
        [Required]
        public string ticker { get; set; }
        [DisplayName("Recent Volatility")]
        public decimal recent_volatility { get; set; }
        [DisplayName("List/ Segment")]
        [Required]
        public string list_segment { get; set; }
        [DisplayName("Issuer")]
        [Required]
        public string issuer { get; set; }
        [DisplayName("Nominal value")]
        [Required]
        public decimal nominal_value { get; set; }
        [DisplayName("Total securities")]
        [Required]
        public int total_number_of_securities { get; set; }
        [DisplayName("Listed securities")]
        [Required]
        public int listed_securities_number { get; set; }
        [DisplayName("Listing date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime listing_date { get; set; }
        [DisplayName("Company")]
        [Required]
        public int fk_Company { get; set; }

        public IList<SelectListItem> CompaniesList { get; set; }

    }
}