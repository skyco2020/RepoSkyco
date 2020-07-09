using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Cards
    {
        [Key]
        public Int64 idcard { get; set; }
        public Int64 idtoken { get; set; }
        public String id { get; set; }
        public Int32 exp_month { get; set; }
        public Int32 exp_year { get; set; }
        public String address_city { get; set; }
        public String address_country { get; set; }
        public String address_line1 { get; set; }
        public String address_line1_check { get; set; }
        public String address_line2 { get; set; }
        public String address_state { get; set; }
        public String address_zip { get; set; }
        public String address_zip_check { get; set; }
        public String brand { get; set; }
        public String country { get; set; }
        public String cvc_check { get; set; }
        public String dynamic_last4 { get; set; }
        public String funding { get; set; }
        public String last4 { get; set; }
        public String name { get; set; }
        public String objectcard { get; set; }
        public String tokenization_method { get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("idtoken")]
        public Tokens Tokens { get; set; }
        #endregion

        #region List
        public List<Payments> Payments { get; set; }

        #endregion
    }
}
