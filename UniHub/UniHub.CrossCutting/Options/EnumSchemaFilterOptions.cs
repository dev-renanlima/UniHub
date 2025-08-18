using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniHub.CrossCutting.Options;

public class EnumSchemaFilterOptions : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum = Enum.GetNames(context.Type)
                .Select(name => new Microsoft.OpenApi.Any.OpenApiString(name))
                .Cast<IOpenApiAny>()
                .ToList();

            schema.Type = "string";
            schema.Format = null;
        }
    }
}

