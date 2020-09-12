using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Providers
    {
        [Key]
        public Int64 idProvider { get; set; }
        public Int64 AccountId { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String e_mail { get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("AccountId")]
        public Skyco_Accounts Accounts { get; set; }
        #endregion
    }
}
