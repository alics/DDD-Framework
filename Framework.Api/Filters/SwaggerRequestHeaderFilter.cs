using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Api.Filters
{
    public class SwaggerRequestHeaderFilter : IOperationFilter
    {
        private readonly SwaggerRequestHeaders _headers;

        public SwaggerRequestHeaderFilter(SwaggerRequestHeaders headers)
        {
            _headers = headers;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            foreach (var (key, value) in _headers)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = key,
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString(value)
                    }
                });
            }
        }
    }
}