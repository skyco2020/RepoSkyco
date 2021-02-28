using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo.Class
{
    public class BaseVideo
    {
        public String url { get; set; }
        public Int64 profile_id { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        public Int32 size { get; set; }
    }
}
