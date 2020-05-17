using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{
    public partial class LocationBE
    {
        #region Properties
        public Int64 LocationId { get; set; }

        public Int64 CountryId { get; set; }
        public Int64 ProvinceId { get; set; }
        public Int64 CityId { get; set; }
        public string AddressName { get; set; }
        public string AddressNumber { get; set; }
        public string Appartement { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }

        #endregion

        #region Relation
        public CityBE City { get; set; }
        public CountryBE Country { get; set; }
        public ProvinceBE Provinces { get; set; }
        #endregion
    }
}
