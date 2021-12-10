using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Biblioteca.Configuracao
{
    public static class SwaggerConfiguracaoExtensions
    {
        public static void AdicionarConfiguracaoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Biblioteca",
                    Description = "Api responsável por cadastrar livros",
                    Contact = new OpenApiContact 
                    { 
                        Name = "Rodrigo", 
                        Email = "rodrigo@email.com" 
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { 
                    Description = "Autorizaçãp JWT via header (requisição) utilizando o scheme Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        new string[] { }
                    }
                });
            });
        }

        public static void UseConfiguracaoSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", 
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
