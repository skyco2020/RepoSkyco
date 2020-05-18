using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Skyco_PhoneTemplate : BaseTemplate
    {
        #region Constructor
        private static Skyco_PhoneTemplate _mytemplate;
        private Skyco_PhoneTemplate() { }
        public static Skyco_PhoneTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Skyco_PhoneTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Skyco_Phone.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetSkyco_Phones.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetSkyco_PhonePagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateSkyco_Phone.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteSkyco_Phone.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Skyco_PhonesRelation;
        }
        #endregion

        #region Routes
        public static Link GetSkyco_Phones { get { return new Link("ref", baseaddress + "/Skyco_Skyco_Phones"); } }
        public static Link Skyco_Phone { get { return new Link("self", baseaddress + "/Skyco_Phones/{id}"); } }
        public static Link Skyco_PhonesRelation { get { return new Link("Skyco_Phone", baseaddress + "/Skyco_Phones/{id}"); } }
        public static Link UpdateSkyco_Phone { get { return new Link("update", baseaddress + "/Skyco_Phones/{id}"); } }
        public static Link DeleteSkyco_Phone { get { return new Link("delete", baseaddress + "/Skyco_Phones/{id}"); } }
        public static Link GetSkyco_PhonePagination { get { return new Link("Skyco_Phones", baseaddress + "/Skyco_Phones/{?page}"); } }
        #endregion
    }
}