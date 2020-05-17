using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Exceptions.Handlers
{
    public abstract class BaseExceptionHandler
    {
        private BaseExceptionHandler mychainhandler;

        public BaseExceptionHandler Mychainhandler
        {
            get
            {
                return mychainhandler;
            }

            set
            {
                mychainhandler = value;
            }
        }

        public abstract IApiExceptions HandleExceptions(Exception ex);
    }
}
