using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.ReDoc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeManagementSystemAPI.Util.Swagger
{
    public static class SwaggerExtensionMethods
    {
        public static void AddBearerSecurityDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
        }

        public static void AddBearerSecurityRequirement(this SwaggerGenOptions options)
        {
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        } });
        }

        public static void UseReDocUI(this WebApplication app, string projectName)
        {
            string projectName2 = projectName;
            app.UseReDoc(delegate (ReDocOptions reDoc)
            {
                reDoc.DocumentTitle = projectName2 + " API";
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl = "/swagger/v1/swagger.json";
                reDoc.ExpandResponses("");
            });
        }
    }
}