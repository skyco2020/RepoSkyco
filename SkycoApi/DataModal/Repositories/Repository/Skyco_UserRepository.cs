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
    public class Skyco_UserRepository : SkyCoGenericRepository<Skyco_Users>, ISkyco_UserRepository
    {
        public Skyco_UserRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Update(Skyco_Users entity, List<string> modifiedfields)
        {
            Skyco_Users skcusr = dbcontext.Skyco_User.Find(entity.UserId);

            skcusr.Firstname = entity.Firstname;
            skcusr.Lastname = entity.Lastname;
            skcusr.Gender = entity.Gender;
            skcusr.Address = entity.Address;
            skcusr.NumberAddress = entity.NumberAddress;
            skcusr.DateOfBirth = entity.DateOfBirth;
            skcusr.UpdatedAt = entity.UpdatedAt;
            skcusr.UpdatedBy = entity.UpdatedBy;

            dbcontext.Skyco_User.Attach(skcusr);
            base.Update(skcusr, modifiedfields);
        }

        public override void Delete(Skyco_Users entity, List<string> modifiedfields)
        {
           Skyco_Users skcusr = dbcontext.Skyco_User.Find(entity.UserId);

            skcusr.Voided = entity.Voided;
            skcusr.VoidedAt = entity.VoidedAt;
            skcusr.VoidedBy = entity.VoidedBy;

            dbcontext.Skyco_User.Attach(skcusr);

            base.Delete(skcusr, modifiedfields);
        }
    }
}
