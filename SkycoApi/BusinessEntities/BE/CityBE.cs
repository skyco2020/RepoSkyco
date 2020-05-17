using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{  
    public partial class CityBE
    {
        #region Properties
        public Int64 CityId { get; set; }

        public Int64 ProvinceId { get; set; }
        public string CityName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public byte? Voided { get; set; }
        public string VoidedBy { get; set; }
        #endregion

        #region Relation
        public ProvinceBE Provinces { get; set; }
        #endregion

        #region List
        public List<LocationBE> Location { get; set; }
        #endregion
    }
}
