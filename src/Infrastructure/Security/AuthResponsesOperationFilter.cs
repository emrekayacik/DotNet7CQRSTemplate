using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Security;

public class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes.Any())
        {
            var securityRequirement = new OpenApiSecurityRequirement()
                {
                    {
                        // Put here you own security scheme, this one is an example
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                            Type = SecuritySchemeType.ApiKey,
                            BearerFormat = "JWT"
                        },
                        new List<string>()
                    }
                };

            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }
}
