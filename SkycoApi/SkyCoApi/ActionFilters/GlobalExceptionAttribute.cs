using Resolver.Exceptions;
using SkyCoApi.ErrorHelper;
using SkyCoApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using System.Web.Management;

namespace SkyCoApi.ActionFilters
{
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            //var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            //trace.Error(context.Request, "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = HttpStatusCode.BadRequest.ToString(),
                };

                GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                trace.Error(context.Request, "Controller : "
               + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
               Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception
               + Environment.NewLine + "Status Code: " + HttpStatusCode.BadRequest
               + Environment.NewLine + "ReasonPhrase: " + HttpStatusCode.BadRequest.ToString()
               + Environment.NewLine + "Message: " + context.ActionContext.ModelState);
                throw new HttpResponseException(resp);
            }


            //else if (exceptionType == typeof(UnauthorizedAccessException))
            //{
            //    var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            //    { Content = new StringContent(context.Exception.Message),
            //        ReasonPhrase = HttpStatusCode.Unauthorized.ToString(), StatusCode = HttpStatusCode.Unauthorized
            //    };
            //    throw new HttpResponseException(resp);
            //}
            else if (exceptionType == typeof(ApiException))
            {
                var webapiException = context.Exception as ApiException;
                if (webapiException != null)
                {

                    JsonCustomException jsonexception = ConverterToJsonException.ConvertToReadableException(webapiException);
                    var resp = new HttpResponseMessage(webapiException.HttpStatus)
                    {

                        Content = new StringContent(jsonexception.ToJSON(), new System.Text.UTF8Encoding(),
                     "application/hal+json"),
                        ReasonPhrase = jsonexception.ReasonPhrase,
                        StatusCode = jsonexception.HttpStatus
                    };
                    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                    trace.Error(context.Request, "Controller : "
                   + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                   Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception
                   + Environment.NewLine + "Status Code: " + jsonexception.HttpStatus
                   + Environment.NewLine + "Internal Code: " + jsonexception.ErrorCode
                   + Environment.NewLine + "ReasonPhrase: " + jsonexception.HttpStatus.ToString()
                   + Environment.NewLine + "Message: " + jsonexception.ErrorDescription);
                    throw new HttpResponseException(resp);

                }


            }
            else if (exceptionType == typeof(ApiBusinessException))
            {
                var businessException = context.Exception as ApiBusinessException;
                if (businessException != null)
                {
                    JsonCustomException jsonexception = ConverterToJsonException.ConvertToReadableException(businessException);
                    var resp = new HttpResponseMessage(businessException.HttpStatus)
                    {

                        Content = new StringContent(jsonexception.ToJSON(), new System.Text.UTF8Encoding(),
                     "application/hal+json"),
                        ReasonPhrase = jsonexception.ReasonPhrase,
                        StatusCode = jsonexception.HttpStatus
                    };
                    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                    trace.Error(context.Request, "Controller : "
                   + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                   Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception
                   + Environment.NewLine + "Status Code: " + jsonexception.HttpStatus
                   + Environment.NewLine + "Internal Code: " + jsonexception.ErrorCode
                   + Environment.NewLine + "ReasonPhrase: " + jsonexception.HttpStatus.ToString()
                   + Environment.NewLine + "Message: " + jsonexception.ErrorDescription);

                    throw new HttpResponseException(resp);
                }
            }
            else if (exceptionType == typeof(ApiDataException))
            {
                var dataException = context.Exception as ApiDataException;
                if (dataException != null)
                {
                    JsonCustomException jsonexception = ConverterToJsonException.ConvertToReadableException(dataException);
                    var resp = new HttpResponseMessage(dataException.HttpStatus)
                    {

                        Content = new StringContent(jsonexception.ToJSON(), new System.Text.UTF8Encoding(),
                     "application/hal+json"),
                        ReasonPhrase = jsonexception.ReasonPhrase,
                        StatusCode = jsonexception.HttpStatus
                    };
                    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                    trace.Error(context.Request, "Controller : "
                   + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                   Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception
                   + Environment.NewLine + "Status Code: " + jsonexception.HttpStatus
                   + Environment.NewLine + "Internal Code: " + jsonexception.ErrorCode
                   + Environment.NewLine + "ReasonPhrase: " + jsonexception.HttpStatus.ToString()
                   + Environment.NewLine + "Message: " + jsonexception.ErrorDescription);
                    throw new HttpResponseException(resp);
                }

                //throw new HttpResponseException(context.Request.CreateResponse(dataException.HttpStatus, new ServiceStatus() { StatusCode = dataException.ErrorCode, StatusMessage = dataException.ErrorDescription, ReasonPhrase = dataException.ReasonPhrase }));
                //throw new HttpResponseException(context.Request.CreateResponse(dataException.HttpStatus,  "StatusCode = "+dataException.ErrorCode + ", StatusMessage ="+ dataException.ErrorDescription + ", ReasonPhrase ="+ dataException.ReasonPhrase ));


            }
            if (exceptionType == typeof(HttpException))
            {
                var httpException = context.Exception as HttpException;
                if (httpException.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
                {
                    var webapiException = new ApiException
                        (7882, "No se pueden obtener respuestas mayores a 7MB", HttpStatusCode.RequestEntityTooLarge, "NoLink");


                    JsonCustomException jsonexception = ConverterToJsonException.ConvertToReadableException(webapiException);
                    var resp = new HttpResponseMessage(webapiException.HttpStatus)
                    {

                        Content = new StringContent(jsonexception.ToJSON(), new System.Text.UTF8Encoding(),
                     "application/hal+json"),
                        ReasonPhrase = jsonexception.ReasonPhrase,
                        StatusCode = jsonexception.HttpStatus
                    };
                    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                    trace.Error(context.Request, "Controller : "
                   + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                   Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception
                   + Environment.NewLine + "Status Code: " + jsonexception.HttpStatus
                   + Environment.NewLine + "Internal Code: " + jsonexception.ErrorCode
                   + Environment.NewLine + "ReasonPhrase: " + jsonexception.HttpStatus.ToString()
                   + Environment.NewLine + "Message: " + jsonexception.ErrorDescription);
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                trace.Error(context.Request, "Controller : "
               + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
               Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);
                //+ Environment.NewLine + "Status Code: " + jsonexception.HttpStatus
                //+ Environment.NewLine + "Internal Code: " + jsonexception.ErrorCode
                //+ Environment.NewLine + "ReasonPhrase: " + jsonexception.HttpStatus.ToString()
                //+ Environment.NewLine + "Message: " + jsonexception.ErrorDescription);
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));

            }
        }
    }
}