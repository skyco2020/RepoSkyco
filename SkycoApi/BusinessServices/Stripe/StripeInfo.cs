﻿using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Stripe
{
    public class StripeInfo
    {
        public static readonly StripeGateway gateway = new StripeGateway("sk_test_pJiL43vnUyaJT9xOyyG80W4s0096SCKG0c");
    }
}