using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class PaymentTemplate : BaseTemplate
    {
        #region Constructor
        private static PaymentTemplate _mytemplate;
        private PaymentTemplate() { }
        public static PaymentTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new PaymentTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Payment.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetPayments.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetPaymentPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdatePayment.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeletePayment.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return PaymentsRelation;
        }
        #endregion

        #region Routes
        public static Link GetPayments { get { return new Link("ref", baseaddress + "/Payments"); } }
        public static Link Payment { get { return new Link("self", baseaddress + "/Payments/{id}"); } }
        public static Link PaymentsRelation { get { return new Link("Payment", baseaddress + "/Payments/{id}"); } }
        public static Link UpdatePayment { get { return new Link("update", baseaddress + "/Payments/{id}"); } }
        public static Link DeletePayment { get { return new Link("delete", baseaddress + "/Payments/{id}"); } }
        public static Link GetPaymentPagination { get { return new Link("Plans", baseaddress + "/Payments/{?page}"); } }
        #endregion
    }
}