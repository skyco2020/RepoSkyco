using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PricePlanBE
    {
        public string Type { get; set; }
        public string TiersMode { get; set; }
        public string ProductId { get; set; }
        public string Nickname { get; set; }
        public long? UnitAmount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool Livemode { get; set; }
        public bool? Deleted { get; set; }
        public string Currency { get; set; }
        public DateTime Created { get; set; }
        public string BillingScheme { get; set; }
        public bool Active { get; set; }
        public string Object { get; set; }
        public string Id { get; set; }
        public string LookupKey { get; set; }
        public decimal? UnitAmountDecimal { get; set; }
    }
}
