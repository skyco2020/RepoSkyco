using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Skyco_AccountTypeTemplate : BaseTemplate
    {
        #region Single
        private static Skyco_AccountTypeTemplate _mytemplate;
        private Skyco_AccountTypeTemplate() { }
        public static Skyco_AccountTypeTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Skyco_AccountTypeTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Skyco_AccountType.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetSkyco_AccountTypes.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetSkyco_AccountTypePagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateSkyco_AccountType.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteSkyco_AccountType.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Skyco_AccountTypeRelation;
        }
        #endregion

        #region Routes
        public static Link GetSkyco_AccountTypes { get { return new Link("ref", baseaddress + "/Skyco_AccountTypes"); } }
        public static Link Skyco_AccountType { get { return new Link("self", baseaddress + "/Skyco_AccountTypes/{id}"); } }
        public static Link Skyco_AccountTypeRelation { get { return new Link("Skyco_AccountType", baseaddress + "/Skyco_AccountTypes/{id}"); } }
        public static Link UpdateSkyco_AccountType { get { return new Link("update", baseaddress + "/Skyco_AccountTypes/{id}"); } }
        public static Link DeleteSkyco_AccountType { get { return new Link("delete", baseaddress + "/Skyco_AccountTypes/{id}"); } }
        public static Link GetSkyco_AccountTypePagination { get { return new Link("Skyco_AccountTypes", baseaddress + "/Skyco_AccountTypes/{?page}"); } }
        #endregion
    }
}