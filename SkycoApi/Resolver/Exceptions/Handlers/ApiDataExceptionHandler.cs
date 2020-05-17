using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Exceptions.Handlers
{
    public class ApiDataExceptionHandler : BaseExceptionHandler
    {
        static ApiDataExceptionHandler _instance;
        private ApiDataExceptionHandler() { }
        public static ApiDataExceptionHandler GetInstance()
        {
            if (_instance == null)
                _instance = new ApiDataExceptionHandler();
            return _instance;
        }

        public override IApiExceptions HandleExceptions(Exception ex)
        {
            if (ex is ApiDataException)
                return (ApiDataException)ex;
            return Mychainhandler.HandleExceptions(ex);
        }
    }
}
