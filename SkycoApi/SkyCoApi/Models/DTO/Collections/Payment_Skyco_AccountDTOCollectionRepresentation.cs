using SkyCoApi.Models.DTO.Single;
using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Collections
{
    public class Payment_Skyco_AccountDTOCollectionRepresentation : BaseCollectionRepresentation<Payment_Skyco_AccountDTO>
    {
        #region Template
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Payment_Skyco_AccountTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }
        #endregion

        #region Representations
        public Payment_Skyco_AccountDTOCollectionRepresentation(IList<Payment_Skyco_AccountDTO> list) : base(list)
        {
            foreach (var l in list)
            {
                l.CreateUpdateLink();
                l.CreateDeleteLink();
            }
        }

        public Payment_Skyco_AccountDTOCollectionRepresentation(IList<Payment_Skyco_AccountDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
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