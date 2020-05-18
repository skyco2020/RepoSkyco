using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Skyco_UserTemplate : BaseTemplate
    {
        #region Constructor
        private static Skyco_UserTemplate _mytemplate;
        private Skyco_UserTemplate() { }
        public static Skyco_UserTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Skyco_UserTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Skyco_User.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetSkyco_Users.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetSkyco_UserPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateSkyco_User.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteSkyco_User.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Skyco_UserRelation;
        }
        #endregion

        #region Routes
        public static Link GetSkyco_Users { get { return new Link("ref", baseaddress + "/Skyco_Users"); } }
        public static Link Skyco_User { get { return new Link("self", baseaddress + "/Skyco_Users/{id}"); } }
        public static Link Skyco_UserRelation { get { return new Link("Skyco_User", baseaddress + "/Skyco_Users/{id}"); } }
        public static Link UpdateSkyco_User { get { return new Link("update", baseaddress + "/Skyco_Users/{id}"); } }
        public static Link DeleteSkyco_User { get { return new Link("delete", baseaddress + "/Skyco_Users/{id}"); } }
        public static Link GetSkyco_UserPagination { get { return new Link("Skyco_Users", baseaddress + "/Skyco_Users/{?page}"); } }
        #endregion
    }
}