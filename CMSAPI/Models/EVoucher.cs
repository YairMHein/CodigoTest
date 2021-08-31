using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSAPI.Models
{
    public class EVoucher
    {
        public int id { get; set; }
        public int maxlimitbuy { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime expirydate { get; set; }
        public decimal amount { get; set; }
        public string paymentmethod { get; set; }
        public int? discountpercentage { get; set; }
        public int quantity { get; set; }
        public int buytype { get; set; }
        public string image { get; set; }
        public string promocode { get; set; }
        public bool isactive { get; set; }
        public DateTime createdate { get; set; }
        public int maxlimitgift { get; set; }

        public string name { get; set; }
        public string phone { get; set; }
        public int buyquantity { get; set; }
        public string buyerid { get; set; }
    }
}
