using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class ProductDTO
    {
        public Int64 idProduct { get; set; }
        public Int64 AccountId { get; set; }
        public String idproductStripe { get; set; }
        public String name { get; set; }
        public String Description { get; set; }
        public String urlimg { get; set; }
        public Boolean active { get; set; }

        #region Relation
        public Skyco_AccountDTO Accounts { get; set; }
        #endregion
    }
}