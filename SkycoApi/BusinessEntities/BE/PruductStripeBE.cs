using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PruductStripeBE
    {
        public Int64 idProduct { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public Boolean active { get; set; }
        public String urlimg { get; set; }
    }
}
