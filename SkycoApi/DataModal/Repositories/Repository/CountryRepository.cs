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
    public class CountryRepository : SkyCoGenericRepository<Countries>, ICountryRepository
    {
        public CountryRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Countries entity, List<string> modifiedfields)
        {
            Countries country = dbcontext.Country.Find(entity.CountryId);

            country.Voided = entity.Voided;
            country.VoidedAt = entity.VoidedAt;
            country.VoidedBy = entity.VoidedBy;

            dbcontext.Country.Attach(country);
            base.Delete(country, modifiedfields);
        }

        public override void Update(Countries entity, List<string> modifiedfields)
        {
            Countries country = dbcontext.Country.Find(entity.CountryId);

            country.CountryName = entity.CountryName;
            country.CountryCode = entity.CountryCode;
            country.UpdatedBy = entity.UpdatedBy;
            country.UpdatedAt = entity.UpdatedAt;

            dbcontext.Country.Attach(country);
            base.Update(country, modifiedfields);    
        }
    }
}
