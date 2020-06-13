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
    public class CardRepository : SkyCoGenericRepository<Cards>, ICardRepository
    {
        public CardRepository(SkyCoDbContext context) : base(context)
        {
        }
    }
}
