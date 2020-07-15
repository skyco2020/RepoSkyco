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
    public class StripeSubscribeRepository : SkyCoGenericRepository<StripeSubscribes>, IStripeSubscribeRepository
    {
        public StripeSubscribeRepository(SkyCoDbContext context) : base(context)
        {
        }
        public override void Delete(StripeSubscribes entity, List<string> modifiedfields)
        {
            StripeSubscribes PSKco = dbcontext.StripeSubscribes.Find(entity.idStripeSubscribe);
            PSKco.state = entity.state;
            dbcontext.StripeSubscribes.Attach(PSKco);
            base.Delete(PSKco, modifiedfields);
        }

        public override void Update(StripeSubscribes entity, List<string> modifiedfields)
        {
            StripeSubscribes PSKco = dbcontext.StripeSubscribes.Find(entity.idStripeSubscribe);

            PSKco.AccountId = entity.AccountId;
            PSKco.idPlanPriceStripe = entity.idPlanPriceStripe;

            dbcontext.StripeSubscribes.Attach(PSKco);
            base.Update(PSKco, modifiedfields);
        }
    }
}
