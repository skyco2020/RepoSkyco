using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Cryptography
{
    public class Base64Encryption : ICryptography
    {
        #region Singleton

        /// <summary>
        /// Variable of class, type Base64Encryption.
        /// </summary>
        private static Base64Encryption base64Encryption;

        /// <summary>
        /// Private Constractor.
        /// </summary>
        private Base64Encryption()
        { }

        /// <summary>
        /// Method with lazy instantiation.
        /// </summary>
        /// <returns></returns>
        public static Base64Encryption GetInstance()
        {
            if (base64Encryption == null)
            {
                base64Encryption = new Base64Encryption();
            }

            return base64Encryption;
        }

        #endregion

        #region Interface Implementation

        public String Encypt(String stringToCypher)
        {
            byte[] byteString = Encoding.Unicode.GetBytes(stringToCypher);
            return Convert.ToBase64String(byteString);
        }

        public String Decrypt(String stringToDecipher)
        {
            byte[] byteString = Convert.FromBase64String(stringToDecipher);
            return Encoding.Unicode.GetString(byteString);
        }

        #endregion
    }
}
