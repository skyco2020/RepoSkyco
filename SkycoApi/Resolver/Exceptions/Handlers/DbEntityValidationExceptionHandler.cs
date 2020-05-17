using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Exceptions.Handlers
{
    public class DbEntityValidationExceptionHandler : BaseExceptionHandler
    {
        static DbEntityValidationExceptionHandler _instance;
        private DbEntityValidationExceptionHandler() { }

        public static DbEntityValidationExceptionHandler GetInstance()
        {
            if (_instance == null)
                _instance = new DbEntityValidationExceptionHandler();
            return _instance;
        }

        public override IApiExceptions HandleExceptions(Exception ex)
        {
            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException e = (DbEntityValidationException)ex;
                string outputLines = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines += string.Format(
                        "{0}: La entidad del tipo \"{1}\" con estado \"{2}\" tiene los siguientes errores:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines += string.Format((string.Format("- Propiedad: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage)));
                    }
                }
                return new ApiDataException(e.HResult, outputLines, System.Net.HttpStatusCode.InternalServerError, "Http");
            }
            return Mychainhandler.HandleExceptions(ex);


        }
    }
}
