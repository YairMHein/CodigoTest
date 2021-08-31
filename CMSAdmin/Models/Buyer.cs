using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSAdmin.Models
{
    public class Buyer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int? maxlimit { get; set; }
        public int? giftuserlimit { get; set; }
    }
}
