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
    public class PaymentRepository : SkyCoGenericRepository<Payments>, IPaymentRepository
    {
        public PaymentRepository(SkyCoDbContext context) : base(context)
        {
        }
        public override void Delete(Payments entity, List<string> modifiedfields)
        {
            Payments payment = dbcontext.Payments.Find(entity.idpayment);
            payment.state = entity.state;
            dbcontext.Payments.Attach(payment);
            base.Delete(payment, modifiedfields);
        }

        public override void Update(Payments entity, List<string> modifiedfields)
        {
            Payments payment = dbcontext.Payments.Find(entity.idpayment);

            payment.idcard = entity.idcard;
            payment.PlanId = entity.PlanId;
            payment.Quantity = entity.Quantity;
            payment.Currency = entity.Currency;
            payment.Description = entity.Description;
            payment.name = entity.name;

            dbcontext.Payments.Attach(payment);
            base.Update(payment, modifiedfields);
        }
    }
}
