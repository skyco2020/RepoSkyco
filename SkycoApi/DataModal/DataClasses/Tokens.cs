using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Tokens
    {
        [Key]
        public Int64 idtoken { get; set; }
        public String id { get; set; }
        public String client_ip { get; set; }
        public Int64 created { get; set; }
        public Boolean livemode { get; set; }
        public String objectcart { get; set; }
        public String type { get; set; }
        public Boolean used { get; set; }
        public Int32 state { get; set; }

        #region List
        public List<Cards> cards { get; set; }
        #endregion
    }
}
