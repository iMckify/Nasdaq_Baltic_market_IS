using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Securities_operation_specialist
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public decimal salary { get; set; }
        public int telephone { get; set; }
        public string fax { get; set; }
        public int fk_Management_board_member { get; set; }
    }
}