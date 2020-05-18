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
            foreach (var l in list)
            {
                l.CreateUpdateLink();
                l.CreateDeleteLink();
            }
        }

        public Skyco_UserDTOCollectionRepresentation(IList<Skyco_UserDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
        {
            foreach (var l in list)
            {
                l.CreateUpdateLink();
                l.CreateDeleteLink();
            }
        }
        #endregion
    }
}