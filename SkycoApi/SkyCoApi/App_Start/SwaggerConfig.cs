using System.Web.Http;
using WebActivatorEx;
using SkyCoApi;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;
using System;
using System.Reflection;
using System.IO;
using SkyCoApi.App_Start.Swagger;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SkyCoApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\bin\";
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                    c.SingleApiVersion("v1", "WebApiSegura con soporte Token JWT")
                     .Description("Sky co")
                          .TermsOfService("Some terms")
                          .Contact(cc => cc
                          .Name("Sky co")
                          .Url("https://somostechies.com/contact")
                          .Email("skyco2020streaming@gmail.com"))
                          .License(lc => lc
                          .Name("Some License")
                          .Url("https://somostechies.com/license"));
                    //c.DocumentFilter<AuthTokenOperation>();

                    c.OperationFilter<HalResponseType>();
                    c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                    c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi();
        }
          /// <summary>
        /// AuthorizationHeaderParameterOperationFilter para introducir JWT en dialogo Swagger
        /// </summary>
        public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();

                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "JWT Token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}
