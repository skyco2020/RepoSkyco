using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class TokenBE:BaseBE
    {
        public Int64 id { get; set; }
        public Int64 client_ip { get; set; }
        public Int64 created { get; set; }
        public Int64 livemode { get; set; }
        public Int64 objectcart { get; set; }
        public Int64 type { get; set; }
        public Int64 used { get; set; }

        #region List
        public List<CardBE> cards { get; set; }
        #endregion
    }
}
