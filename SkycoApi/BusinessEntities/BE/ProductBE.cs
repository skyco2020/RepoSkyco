using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class ProductBE:BaseBE
    {
        public Int64 AccountId { get; set; }
        public String idproductStripe { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String urlimg { get; set; }
        public Boolean active { get; set; }

        #region Relation
        public Skyco_AccountBE Accounts { get; set; }
        #endregion
    }
}
