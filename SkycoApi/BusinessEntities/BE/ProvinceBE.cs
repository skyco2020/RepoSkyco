using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{
    public partial class ProvinceBE
    {
        #region Properties
        public Int64 ProvinceId { get; set; }

        public Int64 CountryId { get; set; }
        public string ProvinceName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region Relation
        public CountryBE Country { get; set; }
        #endregion

        #region List
        public List<CityBE> City { get; set; }
        public List<LocationBE> Location { get; set; }
        #endregion

    }
}
