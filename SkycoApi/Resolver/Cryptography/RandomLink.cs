using Resolver.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Cryptography
{
    public class RandomLink
    {
        public static String GetRandomLink()
        {
            //String verifyPath = MailConfiguration.GetInstance().AppSettings["server_pathverify"].ToString();
            String clave = MD5Encryption.GetInstance().Encypt(Guid.NewGuid().ToString());
            return (getVerifyPath() + clave);
        }

        public static string getVerifyPath()
        {
            return MailConfiguration.GetInstance().AppSettings["server_pathverify"].ToString();
        }

        public static String GetRandomLinkResetPsw()
        {
            String clave = MD5Encryption.GetInstance().Encypt(Guid.NewGuid().ToString());
            return (getVerifyPathResetPsw() + clave);
        }

        public static string getVerifyPathResetPsw()
        {
            return MailConfiguration.GetInstance().AppSettings["server_pathresetpsw"].ToString();
        }            
    }
}
