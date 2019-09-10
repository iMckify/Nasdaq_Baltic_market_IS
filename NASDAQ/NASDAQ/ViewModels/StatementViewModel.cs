using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NASDAQ.ViewModels;

namespace NASDAQ.ViewModels
{
    public class StatementViewModel
    {
        public List<PriceConsolidatedViewModel> prices { get; set; }
        public int amount { get; set; }
        public decimal sum { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? from { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? to { get; set; }
    }
}