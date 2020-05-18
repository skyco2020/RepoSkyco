using Resolver.Exceptions;
using SkyCoApi.ErrorHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.ActionFilters
{
    public static class ConverterToJsonException
    {
        public static JsonCustomException ConvertToReadableException(IApiExceptions ex)
        {
            return new ErrorHelper.JsonCustomException()
            {
                ErrorCode = ex.ErrorCode,
                ErrorDescription = ex.ErrorDescription,
                HttpStatus = ex.HttpStatus,
                ReasonPhrase = ex.ReasonPhrase,
                ReferenceLink = ex.ReferenceLink
            };

        }
    }
}