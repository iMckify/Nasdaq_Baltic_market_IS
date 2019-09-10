using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASDAQ.Models
{
    public class Execute
    {
        public int fk_Order { get; set; }
        public int fk_Securities_operation_specialist { get; set; }
    }
}