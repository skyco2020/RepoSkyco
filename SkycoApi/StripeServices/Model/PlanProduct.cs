using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Model
{
    public class PlanProduct
    {
        public Int64 AccountId { get; set; }
        public String idProductStripe { get; set; }
        public Int64 Price { get; set; }
        public String Description { get; set; }
        public String TypePlan { get; set; }
        public DateTime PlanDate { get; set; }
    }
}
