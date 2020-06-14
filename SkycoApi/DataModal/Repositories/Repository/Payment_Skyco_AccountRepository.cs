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
    public class Payment_Skyco_AccountRepository : SkyCoGenericRepository<Payment_Skyco_Accounts>, IPayment_Skyco_AccountRepository
    {
        public Payment_Skyco_AccountRepository(SkyCoDbContext context) : base(context)
        {
        }
        public override void Delete(Payment_Skyco_Accounts entity, List<string> modifiedfields)
        {
            Payment_Skyco_Accounts PSKco = dbcontext.Payment_Skyco_Accounts.Find(entity.IdPaymentUser);
            PSKco.state = entity.state;
            dbcontext.Payment_Skyco_Accounts.Attach(PSKco);
            base.Delete(PSKco, modifiedfields);
        }

        public override void Update(Payment_Skyco_Accounts entity, List<string> modifiedfields)
        {
            Payment_Skyco_Accounts PSKco = dbcontext.Payment_Skyco_Accounts.Find(entity.IdPaymentUser);

            PSKco.AccountId = entity.AccountId;
            PSKco.Amount = entity.Amount;
            PSKco.idstripecard = entity.idstripecard;
            PSKco.paymentdate = entity.paymentdate;

            dbcontext.Payment_Skyco_Accounts.Attach(PSKco);
            base.Update(PSKco, modifiedfields);
        }
    }
}
