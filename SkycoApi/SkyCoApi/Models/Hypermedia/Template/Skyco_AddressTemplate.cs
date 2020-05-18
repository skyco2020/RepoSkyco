using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class Skyco_AddressTemplate : BaseTemplate
    {
        #region Constructor
        private static Skyco_AddressTemplate _mytemplate;
        private Skyco_AddressTemplate() { }
        public static Skyco_AddressTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new Skyco_AddressTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Skyco_Address.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetSkyco_Addresses.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetSkyco_AddressPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateSkyco_Address.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteSkyco_Address.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return Skyco_AddressRelation;
        }
        #endregion

        #region Routes
        public static Link GetSkyco_Addresses { get { return new Link("ref", baseaddress + "/Skyco_Addresses"); } }
        public static Link Skyco_Address { get { return new Link("self", baseaddress + "/Skyco_Addresses/{id}"); } }
        public static Link Skyco_AddressRelation { get { return new Link("Skyco_Address", baseaddress + "/Skyco_Addresses/{id}"); } }
        public static Link UpdateSkyco_Address { get { return new Link("update", baseaddress + "/Skyco_Addresses/{id}"); } }
        public static Link DeleteSkyco_Address { get { return new Link("delete", baseaddress + "/Skyco_Addresses/{id}"); } }
        public static Link GetSkyco_AddressPagination { get { return new Link("Skyco_Addresses", baseaddress + "/Skyco_Addresses/{?page}"); } }
        #endregion
    }
}