using Biblioteca.Configuracao;
using Biblioteca.Context;
using Biblioteca.Dados.Repositorio;
using Biblioteca.Dados.Repositorio.Interfaces;
using Biblioteca.Services.Auth.Jwt;
using Biblioteca.Services.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
            #region Auth

            services.AddConfiguracaoAuth(Configuration);

            #endregion

            #region JWTSettings

            var sessaoJwt = Configuration.GetSection("JwtConfiguracoes");
            services.Configure<JwtConfiguracoes>(sessaoJwt);

            #endregion

            #region JWTManagement

            services.AddScoped<IJwtAuthGerenciador, JwtAuthGerenciador>();

            #endregion

            #region AuthSettings

            services.AddConfiguracaoAuth(Configuration);

            #endregion

            #region Repositories

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            #endregion

            #region VersioningSettings

            services.AdicinarVersionamentoAPI();

            #endregion

            #region SwaggerDocumentacaoApi

            services.AdicionarConfiguracaoSwagger();

            #endregion

            #region ContextsEFCore

            services.AddDbContext<BibliotecaDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region CorsConfigurantion

            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            #endregion

            #region Controllers

            services.AddControllers();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseCors("Development");

            app.UseConfiguracaoSwagger(provider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
