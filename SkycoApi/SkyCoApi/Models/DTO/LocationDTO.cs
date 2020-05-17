using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO
{
    public class LocationDTO
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
        public CityDTO City { get; set; }
        public CountryDTO Country { get; set; }
        public ProvinceDTO Provinces { get; set; }
        #endregion
    }
}
