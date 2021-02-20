using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Model
{
    public class PlanStripe
    {
        public Int64 PlanId { get; set; }
        public String idProduct { get; set; }
        public Int64 AccountId { get; set; }
        public String idplanstripe { get; set; }
        public String TypePlan { get; set; }
        public Int64 Price { get; set; }
        public String Description { get; set; }
        public DateTime PlanDate { get; set; }
        public Int32 State { get; set; }
    }
}
