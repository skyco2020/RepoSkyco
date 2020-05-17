using DataModal.DataClasses;
using DataModal.GenericRepository;
using DataModal.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Repository
{
    public class ProvinceRepository : SkyCoGenericRepository<Provinces>, IProvinceRepository
    {
        public ProvinceRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Provinces entity, List<string> modifiedfields)
        {
            Provinces province = dbcontext.Provinces.Find(entity.ProvinceId);

            province.Voided = entity.Voided;
            province.VoidedAt = entity.VoidedAt;
            province.VoidedBy = entity.VoidedBy;

            dbcontext.Provinces.Attach(province);
            base.Delete(province, modifiedfields);
        }

        public override void Update(Provinces entity, List<string> modifiedfields)
        {
            Provinces province = dbcontext.Provinces.Find(entity.ProvinceId);

            province.ProvinceName = entity.ProvinceName;
            province.UpdatedAt = entity.UpdatedAt;
            province.UpdatedBy = entity.UpdatedBy;

            dbcontext.Provinces.Attach(province);
            base.Update(province, modifiedfields);
        }
    }
}
