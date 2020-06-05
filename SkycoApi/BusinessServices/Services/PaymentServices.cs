using Autofac.Core;
using BusinessEntities.BE;
using BusinessServices.Interfaces;
using NUnit.Framework;
using ServiceStack.Stripe.Types;
using Stripe;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessServices.Services
{
    public class PaymentServices: IPaymentServices
    {
        protected readonly StripeGateway gateway = new StripeGateway("sk_test_pJiL43vnUyaJT9xOyyG80W4s0096SCKG0c");
        public Boolean Can_Charge_Customer(PaymentBE be)
        {
            //var customer = CreateCustomer();
            try
            {
                var charge = gateway.Post(new ChargeStripeCustomer
                {
                    Amount = be.Amount,
                    Customer = be.idstripecard,
                    Currency = be.Currency,
                    Description = be.Description,
                });

                return true;
            }
            catch (Exception)
            {

                throw;
            }
           

            //charge.PrintDump();

            //Assert.That(charge.Id, Is.Not.Null);
            //Assert.That(charge.Customer, Is.EqualTo(customer.Id));
            //Assert.That(charge.Amount, Is.EqualTo(100));
            ////Assert.That(charge.Source.DynamicLast4, Is.EqualTo("4242"));
            //Assert.That(charge.Paid, Is.True);
        }
    }
}
