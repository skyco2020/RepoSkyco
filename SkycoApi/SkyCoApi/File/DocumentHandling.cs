using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SkyCoApi.File
{
    public class DocumentHandling : FileHandling
    {
        #region Singleton
        private static DocumentHandling handling;
        private DocumentHandling()
        { }

        public static DocumentHandling GetInstance()
        {
            if (handling == null)
            {
                handling = new DocumentHandling();
            }

            return handling;
        }

        public override FileValidation ChargeMyValidator()
        {
            return DocumentValidation.GetInstance();
        }

        #endregion

        public override string getPrivatePath(string username)
        {
            return Path.Combine(FileConfig.Directories["PRINCIPAL_PATH"].ToString(),
                  String.Join(@"\", FileConfig.Directories["DOCUMENT_SUB"].ToString(),
                  username));
        }

        public override string getPublicPath()
        {
            return Path.Combine(FileConfig.Directories["PRINCIPAL_PATH"].ToString(),
                      String.Join(@"\", FileConfig.Directories["PUBLICVIDEO_SUB"].ToString()));
        }
    }
}