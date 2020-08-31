using SkyCoApi.Models.DTO.Single;
using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Collections
{
    public class Skyco_AccountTypeDTOCollectionRepresentation : BaseCollectionRepresentation<Skyco_AccountTypeDTO>
    {
        #region Template
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Skyco_AccountTypeTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }
        #endregion

        #region Representations
        public Skyco_AccountTypeDTOCollectionRepresentation(IList<Skyco_AccountTypeDTO> list) : base(list)
        {
            foreach (Skyco_AccountTypeDTO usr in list)
            {
                usr.CreateUpdateLink();
                usr.CreateDeleteLink();
            }
        }

        public Skyco_AccountTypeDTOCollectionRepresentation(IList<Skyco_AccountTypeDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
        {
            foreach (Skyco_AccountTypeDTO usr in list)
            {
                usr.CreateUpdateLink();
                usr.CreateDeleteLink();
            }
        }
        #endregion
    }
}