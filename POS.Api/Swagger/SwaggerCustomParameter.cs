using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using POS.Utility;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace POS.Api.Swagger
{
    public class SwaggerCustomParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if(descriptor!=null && !descriptor.ControllerName.StartsWith("Authenticate"))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = ApplicationConstants.AUTH_HEADER,
                    In = ParameterLocation.Header,
                    Description = "Authentication Token",
                });
            }
        }
    }
}
