using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NASDAQ.ViewModels
{
    public class PriceEditViewModel
    {
        [DisplayName("From")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        [DisplayName("Price")]
        [Required]
        public decimal value { get; set; }
        [DisplayName("To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime to_date { get; set; }
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Ticker")]
        [Required]
        public string fk_Security { get; set; }

        public IList<SelectListItem> SecuritiesList { get; set; }
    }
}