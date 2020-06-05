using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PaymentIntentBE
    {
        public enum Currency
        {
            usd,
            eur
        }

        public String description { get; set; }
        public Int32 amount { get; set; }
        public Currency currency { get; set; }
    }
}
