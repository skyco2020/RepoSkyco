using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace SkyCoApi.File
{
    public abstract class FileValidation
    {
        public void CompleteValidations(HttpFileCollection files)
        {
            HttpPostedFile file;

            if (files == null || files.Count < 1)
                throw new Exception("There is no file");

            for (int i = 0; i < files.Count; i++)
            {
                file = files[i];
                if (String.IsNullOrEmpty(file.FileName))
                {
                    throw new Exception("File name not entered");
                }

                if (file.ContentLength == 0)
                {
                    throw new Exception("File does not exist");
                }

                String extention = file.FileName.Split('.').LastOrDefault();

                if (extention == null)
                {
                    throw new Exception("The file has no Extension");
                }

                this.ExtensionsValidations(file.FileName);
            }
        }
        public abstract void ExtensionsValidations(String extension);

        public void ValidateDownloadDirectory(DirectoryInfo directoryInfo, string filePath)
        {
            if (!directoryInfo.Exists)
                throw new Exception(filePath + "It is not a valid route");
        }
        public string GetMimeMapping(string filename)
        {
            return MimeMapping.GetMimeMapping(filename);
        }
        //public void ValidateFileExist(string path)
        //{
        //    if (!File.Exists(path))
        //    {
        //        throw new Exception("Image doesn't exist");
        //    }
        //}
    }
}