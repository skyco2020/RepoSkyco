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
    public class Skyco_AccountTypeRepository : SkyCoGenericRepository<Skyco_AccountTypes>, ISkyco_AccountTypeRepository
    {
        public Skyco_AccountTypeRepository(SkyCoDbContext context) : base(context)
        {
        }
        public override void Delete(Skyco_AccountTypes entity, List<string> modifiedfields)
        {
            Skyco_AccountTypes sktype = dbcontext.Skyco_AccountType.Find(entity.AccountTypeId);

            sktype.Voided = entity.Voided;
            sktype.VoidedAt = entity.VoidedAt;
            sktype.VoidedBy = entity.VoidedBy;
            dbcontext.Skyco_AccountType.Attach(sktype);

            base.Delete(sktype, modifiedfields);    
        }
        public override void Update(Skyco_AccountTypes entity, List<string> modifiedfields)
        {
            Skyco_AccountTypes sktype = dbcontext.Skyco_AccountType.Find(entity.AccountTypeId);

            sktype.AccountTypeName = entity.AccountTypeName;
            sktype.UpdatedBy = entity.UpdatedBy;
            sktype.UpdatedAt = entity.UpdatedAt;
            dbcontext.Skyco_AccountType.Attach(sktype);

            base.Update(sktype, modifiedfields);
        }
    }
}
