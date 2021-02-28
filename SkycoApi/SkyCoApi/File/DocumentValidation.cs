using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.File
{
    public class DocumentValidation : FileValidation
    {

        #region Singleton

        /// <summary>
        /// Variable of class, type DocumentValidation.
        /// </summary>
        private static DocumentValidation documentValidation;

        /// <summary>
        /// Private Constractor.
        /// </summary>
        private DocumentValidation()
        { }

        /// <summary>
        /// Method with lazy instantiation.
        /// </summary>
        /// <returns></returns>
        public static DocumentValidation GetInstance()
        {
            if (documentValidation == null)
            {
                documentValidation = new DocumentValidation();
            }

            return documentValidation;
        }

        #endregion

        #region Implementation Method

        public override void ExtensionsValidations(string filename)
        {
            String mimeType = this.GetMimeMapping(filename);
            if (mimeType != FileConfig.MimesSupported["MIME_DOC"].ToString() &&
                mimeType != FileConfig.MimesSupported["MIME_DOCX"].ToString() &&
                mimeType != FileConfig.MimesSupported["MIME_ODT"].ToString() &&
                mimeType != FileConfig.MimesSupported["MIME_PDF"].ToString() &&
                mimeType != FileConfig.MimesSupported["MIME_VIDEO"].ToString())
                throw new Exception(filename + "It has an invalid type for the documents, the allowed ones are: DOC, DOCX, ODT and PDF and video");
        }


        #endregion
    }
}