using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.File
{
    public class FileResult
    {
        public string LocalFilePath { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
    }
}