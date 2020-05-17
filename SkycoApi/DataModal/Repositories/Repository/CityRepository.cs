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
    public class CityRepository : SkyCoGenericRepository<Cities>, ICityRepository
    {
        public CityRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Cities entity, List<string> modifiedfields)
        {
            Cities city = dbcontext.City.Find(entity.CityId);

            city.Voided = entity.Voided;
            city.VoidedAt = entity.VoidedAt;
            city.VoidedBy = entity.VoidedBy;

            dbcontext.City.Attach(city);
            base.Delete(city, modifiedfields);
        }

        public override void Update(Cities entity, List<string> modifiedfields)
        {
            Cities city = dbcontext.City.Find(entity.CityId);

            city.CityName = entity.CityName;
            city.UpdatedAt = entity.UpdatedAt;
            city.UpdatedBy = entity.UpdatedBy;

            dbcontext.City.Attach(city);
            base.Update(city, modifiedfields);
        }
    }
}
