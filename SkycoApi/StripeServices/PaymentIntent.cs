using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public class PaymentIntent
    {
        public Int64 idPaymentIntent { get; set; }
        public Int64 AccountId { get; set; }
        public Int64 PlanId { get; set; }
        public String stripeTokenId { get; set; }
        public String CardId { get; set; }

        public String cardnumber { get; set; }
        public Int32 month { get; set; }
        public Int32 year { get; set; }
        public String cvc { get; set; }
        public long amount { get; set; }
        public String fullname { get; set; }
        public String Email { get; set; }
        public String Description { get; set; }

        public String Price { get; set; }
        public Int32 state { get; set; }
    }
}
