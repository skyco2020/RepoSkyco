using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SkyCoApi.File
{
    public abstract class FileHandling
    {
        #region Property 
        FileValidation fileValidation;

        public FileValidation FileValidation
        {
            get
            {
                if (fileValidation == null)
                    fileValidation = ChargeMyValidator();
                return fileValidation;
            }

            set
            {
                ;
            }
        }

        public abstract FileValidation ChargeMyValidator();
        #endregion

        public virtual string HandlingUpdatePublicDirectories()
        {
            String uploadPath = this.getPublicPath();

            DirectoryInfo directoryInfo = new DirectoryInfo(uploadPath);

            // Create if not exist.
            this.CreateOrNotDirectory(directoryInfo);

            //uploadPath = uploadPath + "\\" + DateTime.Now.Day + "-" +
            //   DateTime.Now.Month + "-" + DateTime.Now.Year;

            directoryInfo = new DirectoryInfo(uploadPath);
            this.CreateOrNotDirectory(directoryInfo);

            return uploadPath;

        }
        public virtual string HandlingDownloadPublicDirectories(string filename)
        {
            String filePath = this.getPublicPath();
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

            this.FileValidation.ValidateDownloadDirectory(directoryInfo, filePath);
            filename = filename.Substring(filePath.Length);
            filePath = filePath + filename;
            return filePath;
        }
        public virtual string HandlingUpdatePrivateDirectories(string username)
        {
            String uploadPath = this.getPrivatePath(username);

            DirectoryInfo directoryInfo = new DirectoryInfo(uploadPath);
            this.CreateOrNotDirectory(directoryInfo);

            return uploadPath;

        }
        public virtual string HandlingDownloadPrivateDirectories(string username, string filename)
        {
            String filePath = this.getPrivatePath(username);

            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

            this.FileValidation.ValidateDownloadDirectory(directoryInfo, filePath);

            filePath = Path.Combine(filePath,
                  String.Join(@"\", filename));

            return filePath;
        }
        public abstract string getPrivatePath(string username);
        public abstract string getPublicPath();

        public void CreateOrNotDirectory(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
                Directory.CreateDirectory(directoryInfo.FullName);
        }
    }
}