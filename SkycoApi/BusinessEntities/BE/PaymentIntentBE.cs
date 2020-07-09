using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PaymentIntentBE
    {
        public Int64 idPaymentIntent { get; set; }
        public Int64 AccountId { get; set; }
        public Int64 PlanId { get; set; }
        public String TokenID { get; set; }
        public long amount { get; set; }
        public String name { get; set; }
        public Int32 state { get; set; }
    }
}
