using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class HitBE
    {
        public Int64 id { get; set; }
        public String pageURL { get; set; }
        public String type { get; set; }
        public String tags { get; set; }
        public Int32 duration { get; set; }
        public Int64 picture_id { get; set; }
        public Int32 views { get; set; }
        public String userImageURL { get; set; }
        public VideoBE videos { get; set; }
    }
}
