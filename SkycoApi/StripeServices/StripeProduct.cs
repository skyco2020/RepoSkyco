using ServiceStack;
using Stripe;
using StripeServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public class StripeProduct
    {
        public String CreateProduct(PlanProduct proplan)
        {
            try
            {
                #region Secret Key
                Key.SecretKey();
                #endregion

                ProductCreateOptions options = new ProductCreateOptions
                {
                    Name = proplan.TypePlan,
                    Description = proplan.Description,
                    Metadata = new Dictionary<string, string>
                    {
                        {
                            "AccountId", proplan.AccountId.ToString()
                        },
                    },
                };
                ProductService service = new ProductService();
                Product produc = service.Create(options);

                PriceCreateOptions Priceoptions = new PriceCreateOptions
                {
                    UnitAmount = proplan.Price,
                    Currency = "usd",
                    Nickname = proplan.Description,
                    Recurring = new PriceRecurringOptions
                    {
                        Interval = "month",                        
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        {
                            "Price", proplan.Price.ToString()
                        },
                        {
                            "AccountId", proplan.AccountId.ToString()
                        },
                    },
                    Product = produc.Id,
                    LookupKey = "standard_monthly",
                    TransferLookupKey = true,
                };
                PriceService Priceservice = new PriceService();
                Priceservice.Create(Priceoptions);
                return produc.Id;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
