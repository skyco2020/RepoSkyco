using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.File
{
    public class FileConfig
    {
        private static Dictionary<string, string> mimesSupported;
        private static Dictionary<string, string> directories;
        private static Dictionary<string, string> extensions;

        #region Singleton
        public static Dictionary<string, string> MimesSupported
        {
            get
            {
                if (mimesSupported == null)
                    chargeMimes();
                return mimesSupported;
            }

            set
            {
                ;
            }
        }
        public static Dictionary<string, string> Directories
        {
            get
            {
                if (directories == null)
                    chargedirectories();
                return directories;
            }

            set
            {
                directories = value;
            }
        }

        public static Dictionary<string, string> Extensions
        {
            get
            {
                if (extensions == null)
                    chargeextensions();
                return extensions;
            }

            set
            {
                extensions = value;
            }
        }

        private static void chargeextensions()
        {
            extensions = new Dictionary<string, string>();
            extensions.Add("PDF", ".pdf");
            extensions.Add("JPE", ".jpe");
            extensions.Add("JPG", ".jpg");
            extensions.Add("JPEG", ".jpeg");
            extensions.Add("PNG", ".png");
            extensions.Add("DOC", ".doc");
            extensions.Add("DOCX", ".docx");
            extensions.Add("ODT", ".odt");
            extensions.Add("XLS", ".xls");
            extensions.Add("XLSX", ".xlsx");
            extensions.Add("ODS", ".ods");
            extensions.Add("VIDEO", ".*");

        }

        private static void chargedirectories()
        {
            directories = new Dictionary<string, string>();
            directories.Add("PRINCIPAL_PATH", AppDomain.CurrentDomain.BaseDirectory + @"MyFiles");
            directories.Add("PICTURE_SUB", "Pictures");
            directories.Add("DOCUMENT_SUB", "Documents");
            directories.Add("SPREEDSHEET_SUB", "Spreedsheet");
            directories.Add("PUBLICPICTURE_SUB", "PublicPictures");
            directories.Add("PUBLICDOCUMENT_SUB", "PublicDocuments");
            directories.Add("PUBLICSPREEDSHEET_SUB", "PublicSpreedsheet");
            directories.Add("PUBLICVIDEO_SUB", "Publicvideo");
        }

        private static void chargeMimes()
        {
            mimesSupported = new Dictionary<string, string>();
            mimesSupported.Add("MIME_AVI", "video/avi");
            mimesSupported.Add("MIME_JPG", "image/jpeg");
            mimesSupported.Add("MIME_PNG", "image/png");
            mimesSupported.Add("MIME_DOC", "application/msword");
            mimesSupported.Add("MIME_DOCX", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            mimesSupported.Add("MIME_XLS", "application/vnd.ms-excel");
            mimesSupported.Add("MIME_XLSX", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            mimesSupported.Add("MIME_PDF", "application/pdf");
            mimesSupported.Add("MIME_ODT", "application/vnd.oasis.opendocument.text");
            mimesSupported.Add("MIME_ODS", "application/vnd.oasis.opendocument.spreadsheet");

            //Video
            mimesSupported.Add("MIME_VIDEO", "video/*");
            //mimesSupported.Add("MIME_WMV", "video/wmv");
            //mimesSupported.Add("MIME_WMA", "video/asf");
            //mimesSupported.Add("MIME_OGG", "video/ogg");
            //mimesSupported.Add("MIME_MP4", "video/mp4");
            //mimesSupported.Add("MIME_WEBM", "video/webm");
            //mimesSupported.Add("MIME_X-FREEAEC", "application/x-freearc");
            //mimesSupported.Add("MIME_MPEG", "video/mpeg");
        }
        #endregion
    }
}