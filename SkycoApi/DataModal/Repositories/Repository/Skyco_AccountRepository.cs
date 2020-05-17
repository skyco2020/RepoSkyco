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
    public class Skyco_AccountRepository : SkyCoGenericRepository<Skyco_Accounts>, ISkyco_AccountRepository
    {
        public Skyco_AccountRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Skyco_Accounts entity, List<string> modifiedfields)
        {
            Skyco_Accounts skcacc = dbcontext.Skyco_Account.Find(entity.AccountId);

            skcacc.Voided = entity.Voided;
            skcacc.VoidedAt = entity.VoidedAt;
            skcacc.VoidedBy = entity.VoidedBy;

            dbcontext.Skyco_Account.Attach(skcacc);
            base.Delete(skcacc, modifiedfields);
        }
    }
}
