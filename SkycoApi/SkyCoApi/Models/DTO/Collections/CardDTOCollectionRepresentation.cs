using SkyCoApi.Models.DTO.Single;
using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Collections
{
    public class CardDTOCollectionRepresentation : BaseCollectionRepresentation<CardDTO>
    {
        #region Template
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = CardTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }
        #endregion

        #region Representations
        public CardDTOCollectionRepresentation(IList<CardDTO> list) : base(list)
        {
            foreach (var l in list)
            {
                l.CreateUpdateLink();
                l.CreateDeleteLink();
            }
        }

        public CardDTOCollectionRepresentation(IList<CardDTO> list, String filters, Int32 pagenumber, Int32 count, Int32 top) : base(list, filters, pagenumber, count, top)
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