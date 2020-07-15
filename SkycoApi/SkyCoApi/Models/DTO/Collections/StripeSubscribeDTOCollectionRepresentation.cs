using SkyCoApi.Models.DTO.Single;
using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Collections
{
    public class StripeSubscribeDTOCollectionRepresentation : BaseCollectionRepresentation<StripeSubscribeDTO>
    {
        #region Template
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = StripeSubscribeTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }
        #endregion

        #region Representations
        public StripeSubscribeDTOCollectionRepresentation(IList<StripeSubscribeDTO> list) : base(list)
        {
            foreach (var l in list)
            {
                l.CreateUpdateLink();
                l.CreateDeleteLink();
            }
        }

        public StripeSubscribeDTOCollectionRepresentation(IList<StripeSubscribeDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
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