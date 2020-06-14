using DataModal.DataClasses;
using DataModal.GenericRepository;
using DataModal.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Repositories.Repository
{
    public class PlanRepository : SkyCoGenericRepository<Plans>, IPlanRepository
    {
        public PlanRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Plans entity, List<string> modifiedfields)
        {
            Plans plan = dbcontext.Plans.Find(entity.PlanId);
            plan.state = entity.state;
            plan.PlanDate = entity.PlanDate;

            dbcontext.Plans.Attach(plan);
            base.Delete(plan, modifiedfields);
        }

        public override void Update(Plans entity, List<string> modifiedfields)
        {
            Plans plan = dbcontext.Plans.Find(entity.PlanId);
            plan.TypePlan = entity.TypePlan;
            plan.Price = entity.Price;
            plan.Description = entity.Description;

            dbcontext.Plans.Attach(plan);
            base.Update(plan, modifiedfields);    
        }
    }
}
