using BusinessEntities.BE;
using StripeServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Interfaces
{
    public interface IPlanServiceStripe
    {
        List<PlanBE> RetrieveAllplan(int state, int page, int top, string orderBy, string ascending, ref int count);
        Task<dynamic> Retrieveplan(Int64 accoutId);
        Task<dynamic> CreatePlan(PlanBE plan);
        Task<dynamic> UpdatePlan(String priceid, Int64 order_id);
        Task<dynamic> DeletePlan(String PlanId);
    }
}
