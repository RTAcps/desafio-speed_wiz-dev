using Microsoft.AspNetCore.Builder;
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
                    Title = "Livro Shop",
                    Description = "Api responsável por cadastrar livros",
                    Contact = new OpenApiContact { Name = "Livro Shop", Email = "contato@livroshop.com" }
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

        public static void UseConfiguracaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "v1");
            });
        }
    }
}
