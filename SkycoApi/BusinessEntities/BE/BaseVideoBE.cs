using System;

namespace BusinessEntities.BE
{
    public class BaseVideoBE
    {
        public String url { get; set; }
        public Int64 profile_id { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        public Int32 size { get; set; }
    }
}