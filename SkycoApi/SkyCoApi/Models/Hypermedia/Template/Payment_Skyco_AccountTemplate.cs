using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Payment_Skyco_AccountTemplate : BaseTemplate
    {
        #region Constructor
        private static Payment_Skyco_AccountTemplate _mytemplate;
        private Payment_Skyco_AccountTemplate() { }
        public static Payment_Skyco_AccountTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Payment_Skyco_AccountTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Payment_Skyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetPayment_Skyco_Accounts.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetPayment_Skyco_AccountPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdatePayment_Skyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeletePayment_Skyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Payment_Skyco_AccountsRelation;
        }
        #endregion

        #region Routes
        public static Link GetPayment_Skyco_Accounts { get { return new Link("ref", baseaddress + "/Payment_Skyco_Accounts"); } }
        public static Link Payment_Skyco_Account { get { return new Link("self", baseaddress + "/Payment_Skyco_Accounts/{id}"); } }
        public static Link Payment_Skyco_AccountsRelation { get { return new Link("Payment_Skyco_Account", baseaddress + "/Payment_Skyco_Accounts/{id}"); } }
        public static Link UpdatePayment_Skyco_Account { get { return new Link("update", baseaddress + "/Payment_Skyco_Accounts/{id}"); } }
        public static Link DeletePayment_Skyco_Account { get { return new Link("delete", baseaddress + "/Payment_Skyco_Accounts/{id}"); } }
        public static Link GetPayment_Skyco_AccountPagination { get { return new Link("Payment_Skyco_Accounts", baseaddress + "/Payment_Skyco_Accounts/{?page}"); } }
        #endregion
    }
}