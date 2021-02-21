using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Products
    {
        [Key]
        public Int64 idProduct { get; set; }
        public Int64 AccountId { get; set; }
        public String idproductStripe { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String urlimg { get; set; }
        public Boolean active { get; set; }

        #region Relation
        [ForeignKey("AccountId")]
        public Skyco_Accounts Accounts { get; set; }
        #endregion
    }
}
