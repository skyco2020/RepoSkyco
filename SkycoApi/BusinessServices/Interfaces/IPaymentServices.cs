using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPaymentServices
    {
        Boolean Can_Charge_Customer(PaymentBE be);
    }
}
