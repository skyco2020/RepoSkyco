using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class TokenBE:BaseBE
    {
        public String id { get; set; }
        public String client_ip { get; set; }
        public Int64 created { get; set; }
        public Boolean livemode { get; set; }
        public String objectcart { get; set; }
        public String type { get; set; }
        public Boolean used { get; set; }

        #region List
        public List<CardBE> cards { get; set; }
        #endregion
    }
}
