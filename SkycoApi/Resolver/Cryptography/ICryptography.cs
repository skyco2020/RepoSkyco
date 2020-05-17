using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Cryptography
{
    public interface ICryptography
    {/// <summary>
     /// Metodo de encriptacion
     /// </summary>
     /// <param name="stringToCypher"></param>
     /// <returns></returns>
        String Encypt(String stringToCypher);

        /// <summary>
        /// Metodo de desencriptacion
        /// </summary>
        /// <param name="stringToDecipher"></param>
        /// <returns></returns>
        String Decrypt(String stringToDecipher);
    }
}
