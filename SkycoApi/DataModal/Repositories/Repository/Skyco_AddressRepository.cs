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
    public class Skyco_AddressRepository : SkyCoGenericRepository<Skyco_Addresses>, ISkyco_AddressRepository
    {
        public Skyco_AddressRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Skyco_Addresses entity, List<string> modifiedfields)
        {
            Skyco_Addresses skcadd = dbcontext.Skyco_Address.Find(entity.AddressId);

            skcadd.Voided = entity.Voided;
            skcadd.VoidedAt = entity.VoidedAt;
            skcadd.VoidedBy = entity.VoidedBy;

            dbcontext.Skyco_Address.Attach(skcadd);
            base.Delete(skcadd, modifiedfields);
        }

        public override void Update(Skyco_Addresses entity, List<string> modifiedfields)
        {
            Skyco_Addresses skcadd = dbcontext.Skyco_Address.Find(entity.AddressId);

            skcadd.UpdatedAt = entity.UpdatedAt;
            skcadd.UpdatedBy = entity.UpdatedBy;

            dbcontext.Skyco_Address.Attach(skcadd);
            base.Update(skcadd, modifiedfields);
        }
    }
}
