using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Perfils
    {
        [Key]
        public Int64 idPerfil { get; set; }
        public Int64 AccountId { get; set; }
        public String name { get; set; }
        public Boolean complete { get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("AccountId")]
        public Skyco_Accounts Account { get; set; }
        #endregion
    }
}
