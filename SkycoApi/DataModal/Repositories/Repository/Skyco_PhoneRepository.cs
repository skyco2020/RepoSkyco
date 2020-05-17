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
    public class Skyco_PhoneRepository : SkyCoGenericRepository<Skyco_Phones>, ISkyco_PhoneRepository
    {
        public Skyco_PhoneRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Skyco_Phones entity, List<string> modifiedfields)
        {
            Skyco_Phones skcph = dbcontext.Skyco_Phone.Find(entity.IdPhone);

            skcph.Voided = entity.Voided;
            skcph.VoidedAt = entity.VoidedAt;
            skcph.VoidedBy = entity.VoidedBy;

            dbcontext.Skyco_Phone.Attach(skcph);
            base.Delete(skcph, modifiedfields);
        }

        public override void Update(Skyco_Phones entity, List<string> modifiedfields)
        {
            Skyco_Phones skcph = dbcontext.Skyco_Phone.Find(entity.IdPhone);

            skcph.PhoneNumber = entity.PhoneNumber;
            skcph.Preferred = entity.Preferred;
            skcph.UpdatedBy = entity.UpdatedBy;
            skcph.UpdatedAt = entity.UpdatedAt;

            dbcontext.Skyco_Phone.Attach(skcph);
            base.Update(skcph, modifiedfields);
        }
    }
}
