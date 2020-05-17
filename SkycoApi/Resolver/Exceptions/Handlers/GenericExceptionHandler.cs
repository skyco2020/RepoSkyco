using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Exceptions.Handlers
{
    public class GenericExceptionHandler : BaseExceptionHandler
    {
        static GenericExceptionHandler _instance;
        private GenericExceptionHandler() { }
        public static GenericExceptionHandler GetInstance()
        {
            if (_instance == null)
                _instance = new GenericExceptionHandler();
            return _instance;
        }

        public override IApiExceptions HandleExceptions(Exception ex)
        {
            return new ApiException(ex.HResult, ex.Message, System.Net.HttpStatusCode.InternalServerError, "Http");
        }
    }
}
