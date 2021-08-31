using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSAdmin.Models
{
    public class Purchase
    {
        public int id { get; set; }
        public int evoucherid { get; set; }
        public string buyerid { get; set; }
        public DateTime? buydate { get; set; }
        public int buyquantity { get; set; }
        public decimal totalamount { get; set; }
        public string promocode { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public string title { get; set; }
    }
}
