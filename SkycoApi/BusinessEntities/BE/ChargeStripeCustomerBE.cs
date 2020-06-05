using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class ChargeStripeCustomerBE
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Idcustomer { get; set; }
        public string Customer { get; set; }
        public string Card { get; set; }
        public string Description { get; set; }
        public bool? Capture { get; set; }
        public int? ApplicationFee { get; set; }
    }
}
