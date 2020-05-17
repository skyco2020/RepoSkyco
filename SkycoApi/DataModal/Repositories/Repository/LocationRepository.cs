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
    public class LocationRepository : SkyCoGenericRepository<Locations>, ILocationRepository
    {
        public LocationRepository(SkyCoDbContext context) : base(context)
        {
        }
    }
}
