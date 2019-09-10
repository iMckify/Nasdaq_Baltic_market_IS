using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NASDAQ.ViewModels
{
    public class FinancialReportEditViewModel
    {
        [DisplayName("Ticker")]
        [Required]
        public string ticker { get; set; }
        [DisplayName("Reporting Period")]
        [Required]
        public string reporting_period { get; set; }
        [DisplayName("Statement type")]
        [Required]
        public string statement_type { get; set; }
        [DisplayName("Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        [Required]
        public int fk_Company { get; set; }

        public IList<SelectListItem> CompaniesList { get; set; }

    }
}