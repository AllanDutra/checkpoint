using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Checkpoint.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerExtensions(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Checkpoint.API",
                        Version = "v1",
                        Description = "" // TODO: PUT A DESCRIPTION HERE
                    }
                );

                var currentAssembly = Assembly.GetExecutingAssembly();

                var xmlFile = $"{currentAssembly.GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

                var xmlDocs = currentAssembly
                    .GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(
                        a =>
                            Path.Combine(
                                Path.GetDirectoryName(currentAssembly.Location),
                                $"{a.Name}.xml"
                            )
                    )
                    .Where(f => File.Exists(f))
                    .ToArray();

                Array.ForEach(xmlDocs, (d) => c.IncludeXmlComments(d));
            });

            return services;
        }
    }
}
