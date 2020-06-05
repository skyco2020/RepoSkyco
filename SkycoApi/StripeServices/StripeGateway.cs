using ServiceStack;
using ServiceStack.Stripe.Types;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public class StripeGateway : IRestGateway
    {
        private const string BaseUrl = "https://api.stripe.com/v1";
        private const string APIVersion = "2018-02-28";

        public TimeSpan Timeout { get; set; }

        public string Currency { get; set; }

        private string apiKey;
        private string publishableKey;
        private string stripeAccount;
        public ICredentials Credentials { get; set; }
        private string UserAgent { get; set; }


        public StripeGateway(string apiKey, string publishableKey = null, string stripeAccount = null)
        {
            this.apiKey = apiKey;
            this.publishableKey = publishableKey;
            this.stripeAccount = stripeAccount;
            Credentials = new NetworkCredential(apiKey, "");
            Timeout = TimeSpan.FromSeconds(60);
            UserAgent = "servicestack .net stripe v1";
            Currency = Currencies.UnitedStatesDollar;
            JsConfig.InitStatics();

            //https://support.stripe.com/questions/how-do-i-upgrade-my-stripe-integration-from-tls-1-0-to-tls-1-2#dotnet
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        protected virtual void InitRequest(HttpWebRequest req, string method, string idempotencyKey)
        {
            req.Accept = MimeTypes.Json;
            req.Credentials = Credentials;

            if (method == HttpMethods.Post || method == HttpMethods.Put)
                req.ContentType = MimeTypes.FormUrlEncoded;

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
                req.Headers["Idempotency-Key"] = idempotencyKey;

            req.Headers["Stripe-Version"] = APIVersion;

            if (!string.IsNullOrWhiteSpace(stripeAccount))
            {
                req.Headers["Stripe-Account"] = stripeAccount;
            }

            PclExport.Instance.Config(req,
                userAgent: UserAgent,
                timeout: Timeout,
                preAuthenticate: true);
        }
        protected virtual void HandleStripeException(WebException ex)
        {
            string errorBody = ex.GetResponseBody();
            var errorStatus = ex.GetStatus() ?? HttpStatusCode.BadRequest;

            if (ex.IsAny400())
            {
                var result = errorBody.FromJson<StripeErrors>();
                throw new StripeException(result.Error)
                {
                    StatusCode = errorStatus
                };
            }
        }
        protected virtual string Send(string relativeUrl, string method, string body, string idempotencyKey)
        {
            try
            {
                var url = BaseUrl.CombineWith(relativeUrl);
                var response = url.SendStringToUrl(method: method, requestBody: body, requestFilter: req =>
                {
                    InitRequest(req, method, idempotencyKey);
                });

                return response;
            }
            catch (WebException ex)
            {
                HandleStripeException(ex);

                throw;
            }
        }

        protected virtual async Task<string> SendAsync(string relativeUrl, string method, string body, string idempotencyKey)
        {
            try
            {
                var url = BaseUrl.CombineWith(relativeUrl);
                var response = await url.SendStringToUrlAsync(method: method, requestBody: body, requestFilter: req =>
                {
                    InitRequest(req, method, idempotencyKey);
                });

                return response;
            }
            catch (Exception ex)
            {
                if (ex.UnwrapIfSingleException() is WebException webEx)
                    HandleStripeException(webEx);

                throw;
            }
        }

        public class ConfigScope : IDisposable
        {
            private readonly WriteComplexTypeDelegate holdQsStrategy;
            private readonly JsConfigScope jsConfigScope;

            public ConfigScope()
            {
                var config = Config.Defaults;
                config.DateHandler = DateHandler.UnixTime;
                config.PropertyConvention = PropertyConvention.Lenient;
                config.TextCase = TextCase.SnakeCase;
                config.TreatEnumAsInteger = false;

                jsConfigScope = JsConfig.With(config);

                holdQsStrategy = QueryStringSerializer.ComplexTypeStrategy;
                QueryStringSerializer.ComplexTypeStrategy = QueryStringStrategy.FormUrlEncoded;
            }

            public void Dispose()
            {
                QueryStringSerializer.ComplexTypeStrategy = holdQsStrategy;
                jsConfigScope.Dispose();
            }
        }

        public T Send<T>(IReturn<T> request, string method, bool sendRequestBody = true, string idempotencyKey = null)
        {
            using (new ConfigScope())
            {
                var relativeUrl = request.ToUrl(method);
                var body = sendRequestBody ? QueryStringSerializer.SerializeToString(request) : null;

                var json = Send(relativeUrl, method, body, idempotencyKey);

                var response = json.FromJson<T>();
                return response;
            }
        }

        public async Task<T> SendAsync<T>(IReturn<T> request, string method, bool sendRequestBody = true, string idempotencyKey = null)
        {
            string relativeUrl;
            string body;

            using (new ConfigScope())
            {
                relativeUrl = request.ToUrl(method);
                body = sendRequestBody ? QueryStringSerializer.SerializeToString(request) : null;
            }

            var json = await SendAsync(relativeUrl, method, body, idempotencyKey);

            using (new ConfigScope())
            {
                var response = json.FromJson<T>();
                return response;
            }
        }

        private static string GetMethod<T>(IReturn<T> request)
        {
            var method = request is IPost ?
                  HttpMethods.Post
                : request is IPut ?
                  HttpMethods.Put
                : request is IDelete ?
                  HttpMethods.Delete
                : HttpMethods.Get;
            return method;
        }

        public T Send<T>(IReturn<T> request)
        {
            var method = GetMethod(request);
            return Send(request, method, sendRequestBody: method == HttpMethods.Post || method == HttpMethods.Put);
        }

        public Task<T> SendAsync<T>(IReturn<T> request)
        {
            var method = GetMethod(request);
            return SendAsync(request, method, sendRequestBody: method == HttpMethods.Post || method == HttpMethods.Put);
        }

        public T Get<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Get, sendRequestBody: false);
        }

        public Task<T> GetAsync<T>(IReturn<T> request)
        {
            return SendAsync(request, HttpMethods.Get, sendRequestBody: false);
        }

        public T Post<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Post);
        }

        public Task<T> PostAsync<T>(IReturn<T> request)
        {
            return SendAsync(request, HttpMethods.Post);
        }

        public T Post<T>(IReturn<T> request, string idempotencyKey)
        {
            return Send(request, HttpMethods.Post, true, idempotencyKey);
        }

        public Task<T> PostAsync<T>(IReturn<T> request, string idempotencyKey)
        {
            return SendAsync(request, HttpMethods.Post, true, idempotencyKey);
        }

        public T Put<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Put);
        }

        public Task<T> PutAsync<T>(IReturn<T> request)
        {
            return SendAsync(request, HttpMethods.Put);
        }

        public T Delete<T>(IReturn<T> request)
        {
            return Send(request, HttpMethods.Delete, sendRequestBody: false);
        }

        public Task<T> DeleteAsync<T>(IReturn<T> request)
        {
            return SendAsync(request, HttpMethods.Delete, sendRequestBody: false);
        }
    }
}

namespace ServiceStack.Stripe.Types
{
    public class StripeErrors
    {
        public StripeError Error { get; set; }
    }

    public class StripeError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Param { get; set; }
        public string DeclineCode { get; set; }
    }

    public class StripeException : Exception
    {
        public StripeException(StripeError error)
            : base(error.Message)
        {
            Code = error.Code;
            Param = error.Param;
            Type = error.Type;
            DeclineCode = error.DeclineCode;
        }

        public string Code { get; set; }
        public string DeclineCode { get; set; }
        public string Param { get; set; }
        public string Type { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class StripeReference
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
    }

    public class StripeObject
    {
        public StripeType? Object { get; set; }
    }

    public class StripeId : StripeObject
    {
        public string Id { get; set; }
    }

    public enum StripeType
    {
        unknown,
        account,
        card,
        charge,
        coupon,
        customer,
        discount,
        dispute,
        @event,
        invoiceitem,
        invoice,
        line_item,
        plan,
        subscription,
        token,
        transfer,
        list,
        product,
    }

    public class StripeInvoice : StripeId
    {
        public DateTime Date { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public StripeCollection<StripeLineItem> Lines { get; set; }
        public int Subtotal { get; set; }
        public int Total { get; set; }
        public string Customer { get; set; }
        public bool Attempted { get; set; }
        public bool Closed { get; set; }
        public bool Paid { get; set; }
        public bool Livemode { get; set; }
        public int AttemptCount { get; set; }
        public int AmountDue { get; set; }
        public string Currency { get; set; }
        public int StartingBalance { get; set; }
        public int? EndingBalance { get; set; }
        public DateTime? NextPaymentAttempt { get; set; }
        public string Charge { get; set; }
        public StripeDiscount Discount { get; set; }
        public int? ApplicationFee { get; set; }
    }

    public class StripeCollection<T> : StripeId
    {
        public string Url { get; set; }
        public int TotalCount { get; set; }
        public bool? HasMore { get; set; }
        public List<T> Data { get; set; }
    }

    public class ChargeStripeCustomer : IPost, IReturn<StripeCharge>
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Customer { get; set; }
        public string Card { get; set; }
        public string Description { get; set; }
        public bool? Capture { get; set; }
        public int? ApplicationFee { get; set; }
    }
    public class StripeLineItem : StripeId
    {
        public string Type { get; set; }
        public bool Livemode { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public bool Proration { get; set; }
        public StripePeriod Period { get; set; }
        public int? Quantity { get; set; }
        public StripePlan Plan { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class StripeProduct : StripeId
    {
        public bool Active { get; set; }
        public string[] Attributes { get; set; }
        public string Caption { get; set; }
        public DateTime? Created { get; set; }
        public string[] DeactivateOn { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public StripePackageDimensions PackageDimensions { get; set; }
        public bool Shippable { get; set; }
        public StripeCollection<StripeSku> Skus { get; set; }
        public string StatementDescriptor { get; set; }
        public StripeProductType Type { get; set; }
        public DateTime? Updated { get; set; }
        public string Url { get; set; }
    }

    public enum StripeProductType
    {
        good,
        service,
    }

    public class StripePackageDimensions
    {
        /// <summary>
        /// Height in inches
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// Width in inches
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// Weight in inches
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Length in inches
        /// </summary>
        public decimal Length { get; set; }
    }

    public class StripeSku : StripeId
    {
        public bool Active { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public string Image { get; set; }
        public StripeInventory Inventory { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public StripePackageDimensions PackageDimensions { get; set; }
    }

    public class StripeInventory
    {
        public int Quantity { get; set; }
        public StripeInventoryType Type { get; set; }
        public StripeInventoryValue Value { get; set; }
    }

    public enum StripeInventoryType
    {
        finite,
        bucket,
        infinite,
    }

    public enum StripeInventoryValue
    {
        in_stock,
        limited,
        out_of_stock,
    }

    public class StripePlan : StripeId
    {
        public int Amount { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public StripePlanInterval Interval { get; set; }
        public int? IntervalCount { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Nickname { get; set; }
        public string Product { get; set; }
        public int? TrialPeriodDays { get; set; }
    }

    public class StripePlanProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StatementDescriptor { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class StripePeriod
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public enum StripePlanInterval
    {
        month,
        year
    }

    public class StripeDiscount : StripeId
    {
        public string Customer { get; set; }
        public StripeCoupon Coupon { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }

    public class StripeCoupon : StripeId
    {
        public int? AmountOff { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public StripeCouponDuration Duration { get; set; }
        public int? DurationInMonths { get; set; }
        public bool Livemode { get; set; }
        public int? MaxRedemptions { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int? PercentOff { get; set; }
        public DateTime? RedeemBy { get; set; }
        public int TimesRedeemed { get; set; }
        public bool Valid { get; set; }
    }

    public enum StripeCouponDuration
    {
        forever,
        once,
        repeating
    }

    public class StripeCustomer : StripeId
    {
        public int AccountBalance { get; set; }
        public string BusinessVatId { get; set; }
        public DateTime? Created { get; set; }
        public string DefaultSource { get; set; }
        public bool? Delinquent { get; set; }
        public string Description { get; set; }
        public StripeDiscount Discount { get; set; }
        public string Email { get; set; }
        public string InvoicePrefix { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public StripeCollection<StripeCard> Sources { get; set; }
        public StripeCollection<StripeSubscription> Subscriptions { get; set; }
        public bool Deleted { get; set; }
        public string Currency { get; set; }
    }

    public class StripeDateRange
    {
        public DateTime? Gt { get; set; }
        public DateTime? Gte { get; set; }
        public DateTime? Lt { get; set; }
        public DateTime? Lte { get; set; }
    }

    public class StripeCard : StripeId
    {
        public StripeCard()
        {
            this.Object = StripeType.card;
        }

        public string Brand { get; set; }
        public string Number { get; set; }
        public string Last4 { get; set; }
        public string DynamicLast4 { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Name { get; set; }

        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressState { get; set; }
        public string AddressZip { get; set; }
        public StripeCvcCheck? CvcCheck { get; set; }
        public string AddressLine1Check { get; set; }
        public string AddressZipCheck { get; set; }

        public string Funding { get; set; }

        public string Fingerprint { get; set; }
        public string Customer { get; set; }
        public string Country { get; set; }
    }

    public enum StripeCvcCheck
    {
        Unknown,
        Pass,
        Fail,
        Unchecked
    }

    public class StripeSubscription : StripeId
    {
        public DateTime? CurrentPeriodEnd { get; set; }
        public StripeSubscriptionStatus Status { get; set; }
        public StripePlan Plan { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? TrialStart { get; set; }
        public bool? CancelAtPeriodEnd { get; set; }
        public DateTime? TrialEnd { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public string Customer { get; set; }
        public int Quantity { get; set; }
    }

    public enum StripeSubscriptionStatus
    {
        Unknown,
        Trialing,
        Active,
        PastDue,
        Canceled,
        Unpaid
    }

    public class StripeCharge : StripeId
    {
        public bool LiveMode { get; set; }
        public int Amount { get; set; }
        public bool Captured { get; set; }
        public StripeCard Source { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public bool Paid { get; set; }
        public bool Refunded { get; set; }
        public StripeCollection<StripeRefund> Refunds { get; set; }
        public int AmountRefunded { get; set; }
        public string BalanceTransaction { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public StripeDispute Dispute { get; set; }
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
        public string Invoice { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class CreateStripeCharge : StripeId
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Customer { get; set; }
        public StripeCard Card { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool Capture { get; set; }
        public int? ApplicationFee { get; set; }
    }

    public class GetStripeCharge
    {
        public string Id { get; set; }
    }

    public class UpdateStripeCharge
    {
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class StripeRefund : StripeObject
    {
        public int Amount { get; set; }
        public string Charge { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string BalanceTransaction { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string ReceiptNumber { get; set; }
    }

    public class StripeDispute : StripeObject
    {
        public StripeDisputeStatus Status { get; set; }
        public string Evidence { get; set; }
        public string Charge { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public int Amount;
        public bool LiveMode { get; set; }
        public StripeDisputeReason Reason { get; set; }
        public DateTime? EvidenceDueBy { get; set; }
    }

    public class StripeFeeDetail
    {
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Application { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }

    public enum StripeDisputeStatus
    {
        Won,
        Lost,
        NeedsResponse,
        UnderReview
    }

    public enum StripeDisputeReason
    {
        Duplicate,
        Fraudulent,
        SubscriptionCanceled,
        ProductUnacceptable,
        ProductNotReceived,
        Unrecognized,
        CreditNotProcessed,
        General
    } 
}
