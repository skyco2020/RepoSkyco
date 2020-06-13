﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class CardBE:BaseBE
    {
        public Int64 idtoken { get; set; }
        public String id { get; set; }
        public Int32 exp_month { get; set; }
        public Int32 exp_year { get; set; }
        public String address_city { get; set; }
        public String address_country { get; set; }
        public String address_line1 { get; set; }
        public String address_line1_check { get; set; }
        public String address_line2 { get; set; }
        public String address_state { get; set; }
        public String address_zip { get; set; }
        public String address_zip_check { get; set; }
        public String brand { get; set; }
        public String country { get; set; }
        public String cvc_check { get; set; }
        public String dynamic_last4 { get; set; }
        public String funding { get; set; }
        public String last4 { get; set; }
        public String name { get; set; }
        public String objectcard { get; set; }
        public String tokenization_method { get; set; }
    }
}
