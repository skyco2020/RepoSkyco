using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Exceptions.Handlers
{
    public class HandlerErrorExceptions
    {
		private static HandlerErrorExceptions _handler;
		private HandlerErrorExceptions() { }
		static List<BaseExceptionHandler> Handlers;
		public static HandlerErrorExceptions GetInstance()
		{
			if (_handler == null)
				_handler = new HandlerErrorExceptions();
			return _handler;
		}
		public Exception RunCustomExceptions(Exception ex)
		{
			if (Handlers == null)
				FillExceptions();
			return RunExceptions(ex);


		}

		private Exception RunExceptions(Exception ex)
		{
			return (Exception)Handlers.First().HandleExceptions(ex);
		}
		private void FillExceptions()
		{
			Handlers = new List<BaseExceptionHandler>();
			Handlers.Add(ApiBusinessExceptionHandler.GetInstance());
			Handlers.Add(ApiDataExceptionHandler.GetInstance());
			Handlers.Add(ApiExceptionHandler.GetInstance());
			Handlers.Add(DbEntityValidationExceptionHandler.GetInstance());
			Handlers.Add(GenericExceptionHandler.GetInstance());
			BaseExceptionHandler _handler;
			for (int i = 0; i <= Handlers.Count(); i++)
			{

				_handler = Handlers.ElementAt(i);
				if (i + 1 == Handlers.Count())
					break;
				_handler.Mychainhandler = Handlers.ElementAt(i + 1);

			}
		}
	}
}
