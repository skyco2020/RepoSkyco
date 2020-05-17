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
    }
}
