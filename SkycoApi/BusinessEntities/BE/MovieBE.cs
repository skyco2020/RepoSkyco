using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class MovieBE
    {
        public String urlmovie { get; set; }
        public String total { get; set; }
        public String totalHits { get; set; }
        public List<HitBE> hits { get; set; }
    }
}
