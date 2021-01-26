using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{
    public partial class Skyco_UserBE
    {
        #region Prperties
        public Int64 UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public byte? Gender { get; set; }
        public string Address { get; set; }
        public string NumberAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }

        public string Country { get; set; }

        public string province { get; set; }
        public string message { get; set; }

        public string city { get; set; }

        #endregion

        #region List       
        public List<Skyco_AccountBE> Skyco_Account { get; set; }
        public List<Skyco_AddressBE> Skyco_Address { get; set; }
        public List<Skyco_PhoneBE> Skyco_Phone { get; set; }
        #endregion
    }
}
