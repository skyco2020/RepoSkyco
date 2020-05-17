using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Resolver.Mailing
{
    public abstract class BaseConfiguration
    {
        #region Members

        private NameValueCollection appSettings;
        protected String configPath;

        #endregion

        #region Properties

        protected abstract String ConfigPath
        {
            get;
        }

        public NameValueCollection AppSettings
        {
            get
            {
                if (this.appSettings == null)
                {
                    this.appSettings = new NameValueCollection();
                    this.Load(this.ConfigPath);
                }

                return this.appSettings;
            }
        }

        #endregion

        #region Methods

        private void Load(String path)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            XmlNode xnodes = xdoc.SelectSingleNode("/configuration");

            foreach (XmlNode xnn in xnodes.ChildNodes)
            {
                if (xnn.Attributes != null)
                    this.AppSettings[xnn.Attributes[0].Value] = xnn.Attributes[1].Value;
            }
        }

        #endregion
    }
}
