using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO
{
    public partial class CountryDTO
    {
        #region Properties
        public Int64 CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region List
        public List<LocationDTO> Location { get; set; }
        public List<ProvinceDTO> Provinces { get; set; }
        #endregion
    }
}
