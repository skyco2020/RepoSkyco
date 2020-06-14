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

        public override void Delete(Cards entity, List<string> modifiedfields)
        {
            Cards card = dbcontext.Cards.Find(entity.idcard);
            card.state = entity.state;
            dbcontext.Cards.Attach(card);
            base.Delete(card, modifiedfields);
        }

        public override void Update(Cards entity, List<string> modifiedfields)
        {
            Cards card = dbcontext.Cards.Find(entity.idcard);

            card.id = entity.id;
            card.exp_month = entity.exp_month;
            card.exp_year = entity.exp_year;
            card.address_city = entity.address_city;
            card.address_country = entity.address_country;
            card.address_line1 = entity.address_line1;
            card.address_line1_check = entity.address_line1_check;
            card.address_line2 = entity.address_line2;
            card.address_state = entity.address_state;
            card.address_zip = entity.address_zip;
            card.address_zip_check = entity.address_zip_check;
            card.brand = entity.brand;
            card.country = entity.country;
            card.cvc_check = entity.cvc_check;
            card.dynamic_last4  = entity.dynamic_last4;
            card.funding = entity.funding;
            card.last4 = entity.last4;
            card.name = entity.name;
            card.objectcard = entity.objectcard;
            card.tokenization_method = entity.tokenization_method;

            dbcontext.Cards.Attach(card);
            base.Update(card, modifiedfields);
        }
    }
}
