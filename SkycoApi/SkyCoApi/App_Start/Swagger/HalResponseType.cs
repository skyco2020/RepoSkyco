using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SkyCoApi.App_Start.Swagger
{
    public class HalResponseType : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //if (operation.operationId == "myCustomName")
            //{
            operation.produces.Clear();
            operation.produces.Add("application/hal+json");
            operation.produces.Add("application/json");
            operation.produces.Add("application/hal+xml");
            operation.produces.Add("application/xml");
            //List<string> oper = new List<string>();
            //oper = operation.produces.ToList();

            //oper.pro

            //operation.produces.OrderByDescending(t=>t.Length);
            //}
        }
    }
}