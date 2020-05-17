using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Cryptography
{
    public class MD5Encryption : ICryptography
    {
        #region Singleton

        /// <summary>
        /// Variable of class, type MD5Encryption.
        /// </summary>
        private static MD5Encryption md5Encryption;

        /// <summary>
        /// Private Constractor.
        /// </summary>
        private MD5Encryption()
        { }

        /// <summary>
        /// Method with lazy instantiation.
        /// </summary>
        /// <returns></returns>
        public static MD5Encryption GetInstance()
        {
            if (md5Encryption == null)
            {
                md5Encryption = new MD5Encryption();
            }

            return md5Encryption;
        }

        #endregion

        #region Inteface Implementation

        /// <summary>
        /// No se puede desencriptar de MD5, no sea gil!
        /// </summary>
        /// <param name="stringToDecipher"></param>
        /// <returns></returns>
        public String Decrypt(String stringToDecipher)
        {
            throw new NotImplementedException();
        }

        public String Encypt(string stringToCypher)
        {
            byte[] data = Encoding.UTF8.GetBytes(stringToCypher);
            data = new MD5CryptoServiceProvider().ComputeHash(data);
            string cripter = Convert.ToBase64String(data).Replace('+', '7').Replace('=', Convert.ToChar(new Random(3).Next(1, 9).ToString()));
            return cripter;

        }

        #endregion
    }
}
