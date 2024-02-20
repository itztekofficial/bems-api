using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Admin.Api.Extensions;
/// <summary>
/// SwaggerDocumentationExtensions
/// </summary>
public static class SwaggerDocumentationExtensions
{
    /// <summary>
    /// AddSwaggerDocumentation
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(name: "AdminApi",
                new OpenApiInfo { Title = "Admin API", Version = "v1" });
            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

            options.CustomSchemaIds(i => i.FullName);
            options.DocInclusionPredicate((docName, Description) => true);
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Api", Version = "v1" });

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// UseSwaggerDocumentation
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin.Api");
        });

        return app;
    }
}