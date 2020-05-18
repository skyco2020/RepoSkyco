using SkyCoApi.Models.DTO.Single;
using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Collections
{
    public class Skyco_UserDTOCollectionRepresentation : BaseCollectionRepresentation<Skyco_UserDTO>
    {
        #region Template
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Skyco_UserTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }
        #endregion

        #region Representations
        public Skyco_UserDTOCollectionRepresentation(IList<Skyco_UserDTO> list) : base(list)
        {
            foreach (Skyco_UserDTO usr in list)
            {
                usr.CreateUpdateLink();
                usr.CreateDeleteLink();
                usr.MySkyco_AccountRelations();
                usr.MySkyco_AddressRelations();
                usr.MySkyco_PhoneRelations();
            }
        }

        public Skyco_UserDTOCollectionRepresentation(IList<Skyco_UserDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
        {
            foreach (Skyco_UserDTO usr in list)
            {
                usr.CreateUpdateLink();
                usr.CreateDeleteLink();
                usr.MySkyco_AccountRelations();
                usr.MySkyco_AddressRelations();
                usr.MySkyco_PhoneRelations();
            }
        }
        #endregion
    }
}