using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class VideoBE
    {
        public LargeBE large { get; set; }
        public MediumBE medium { get; set; }
        public SmallBE small { get; set; }
        public TinyBE tiny { get; set; }
    }
}
