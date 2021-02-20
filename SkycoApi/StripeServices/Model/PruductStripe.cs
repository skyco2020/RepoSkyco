using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Model
{
    public class PruductStripe
    {
        public Int64 idProduct { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String urlimg { get; set; }
        public Boolean active { get; set; }
    }
}
