using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NASDAQ.Models
{
    public class Company
    {
        [DisplayName("Code")]
        [Required]
        public int code { get; set; }
        [DisplayName("Name")]
        [Required]
        public string name { get; set; }
        [DisplayName("Value")]
        public int value { get; set; }
        [DisplayName("Number of shares")]
        [Required]
        public int number_of_shares { get; set; }
        [DisplayName("Dividend per share")]
        public decimal dividend_per_share { get; set; }
        [DisplayName("Number of shareholders")]
        public int number_of_shareholders { get; set; }

        /*
        public int code { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int number_of_shares { get; set; }
        public decimal dividend_per_share { get; set; }
        public int number_of_shareholders { get; set; }
        */
    }
}