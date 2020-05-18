using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Skyco_AccountTemplate : BaseTemplate
    {
        #region Constructor
        private static Skyco_AccountTemplate _mytemplate;
        private Skyco_AccountTemplate() { }
        public static Skyco_AccountTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Skyco_AccountTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Skyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetSkyco_Accounts.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetSkyco_AccountPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateSkyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteSkyco_Account.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Skyco_AccountRelation;
        }
        #endregion

        #region Routes
        public static Link GetSkyco_Accounts { get { return new Link("ref", baseaddress + "/Skyco_Accounts"); } }
        public static Link Skyco_Account { get { return new Link("self", baseaddress + "/Skyco_Accounts/{id}"); } }
        public static Link Skyco_AccountRelation { get { return new Link("Skyco_Account", baseaddress + "/Skyco_Accounts/{id}"); } }
        public static Link UpdateSkyco_Account { get { return new Link("update", baseaddress + "/Skyco_Accounts/{id}"); } }
        public static Link DeleteSkyco_Account { get { return new Link("delete", baseaddress + "/Skyco_Accounts/{id}"); } }
        public static Link GetSkyco_AccountPagination { get { return new Link("Skyco_Accounts", baseaddress + "/Skyco_Accounts/{?page}"); } }
        #endregion
    }
}