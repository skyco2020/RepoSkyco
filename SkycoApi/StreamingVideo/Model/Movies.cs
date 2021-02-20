using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo.Model
{
    public class Movies
    {
        public Int64 idMovie { get; set; }
        public String name { get; set; }
        public String urlimg { get; set; }
        public Int32 state { get; set; }
    }
}
