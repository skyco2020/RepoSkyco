﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public class PaymentIntent
    {
        public Int64 idPaymentIntent { get; set; }
        public String Customerid { get; set; }
        public String subscriptionId { get; set; }
        public Int64 AccountId { get; set; }
        public String stripeTokenId { get; set; }
        public String CardId { get; set; }
        public String fullname { get; set; }
        public String Email { get; set; }
        public String Description { get; set; }

        public String IDStripePrice { get; set; }
        public Int32 state { get; set; }
    }
}