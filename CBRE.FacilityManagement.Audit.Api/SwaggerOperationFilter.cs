using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CBRE.FacilityManagement.Audit.API
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = context.ApiDescription.ParameterDescriptions;
            foreach (var parameter in parameters)
            {
                if (parameter.Source == Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource.Query)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = parameter.Name,
                        In = ParameterLocation.Query,
                        Required = parameter.IsRequired,
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    });
                }
            }
        }
    }
}
