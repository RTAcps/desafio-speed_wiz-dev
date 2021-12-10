using Biblioteca.Configuracao;
using Biblioteca.Context;
using Biblioteca.Dados.Repositorio;
using Biblioteca.Dados.Repositorio.Interfaces;
using Biblioteca.Services.Auth.Jwt;
using Biblioteca.Services.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Biblioteca
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Autenticacao
            services.AddConfiguracaoAuth(Configuration);

            //Configurações
            var sessao = Configuration.GetSection("JwtConfiguracoes");
            services.Configure<JwtConfiguracoes>(sessao);

            // Repositórios
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            //Serviços
            services.AddScoped<IJwtAuthGerenciador, JwtAuthGerenciador>();

            // Controllers
            services.AddControllers();

            //Swagger - Documentação Api's
            services.AdicionarConfiguracaoSwagger();

            //Contextos
            services.AddDbContext<BibliotecaDbContext>(options => options.UseInMemoryDatabase(databaseName: "BibliotecaDB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguracaoSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
