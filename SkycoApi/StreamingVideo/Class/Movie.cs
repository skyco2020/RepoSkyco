using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo.Class
{
    public class Movie
    {
        public String urlmovie { get; set; }
        public String total { get; set; }
        public String totalHits { get; set; }
        public List<Hits> hits { get; set; }
    }
}
