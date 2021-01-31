using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Mailing
{
    public class MailConfiguration : BaseConfiguration
    {
        #region Singleton

        /// <summary>
        /// variable of class, type MailConfiguration.
        /// </summary>
        private static MailConfiguration mailConfiguration;

        /// <summary>
        /// Private Constractor.
        /// </summary>
        private MailConfiguration()
        { }

        /// <summary>
        /// Method with lazy instantiation.
        /// </summary>
        /// <returns></returns>
        public static MailConfiguration GetInstance()
        {
            if (mailConfiguration == null)
            {
                mailConfiguration = new MailConfiguration();
            }

            return mailConfiguration;
        }

        #endregion

        #region Properties Implementation

        public string a()
        {
            string b = this.ConfigPath;
            return b;
        }

        protected override string ConfigPath
        {
            get
            {
                if (this.configPath == null)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    this.configPath = path.Remove(path.Length - 9) + @"Resolver\Mailing\Mail.config";
                    //this.configPath = ConfigurationManager.AppSettings["MAIL_PATH"];
                }
                return this.configPath;
            }
        }

        #endregion
    }
}
