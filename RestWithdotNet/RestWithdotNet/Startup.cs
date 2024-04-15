using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWithDotNet.Model.Context;
using RestWithDotNet.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithDotNet.Business;
using RestWithDotNet.Repository;
using Serilog;
using RestWithDotNet.Repository.Generic;
using System.Net.Http.Headers;
using RestWithDotNet.Hypermedia.Filters;
using RestWithDotNet.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using System.Net.Sockets;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithDotNet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // CORS - Permite que a aplicação pegue recursos de outra aplicação com domínios diferentes
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            ));
            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnectionString"];  // mesma que está na appsettings.json
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if(Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true; // para aceitar a propriedade accept setada no cabeçalho da request no Header
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
            })
            .AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            services.AddApiVersioning();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Rest API's from 0 to azure with ASP .Net Core 5 and docker",
                        Version = "v1",
                        Description = "API RESTful developed in course 'Rest API's from 0 to azure with ASP .Net Core 5 and docker'",
                        Contact = new OpenApiContact
                        {
                            Name = "Henrique Soares",
                            Url = new Uri("https://github.com/henriquesph")
                        }
                    });
            });

            // Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<HyperMediaFilterOptions>(); é pra colocar essa injeção?
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(); // só funciona nessa posição

            app.UseSwagger(); // gera o Json

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Rest API's from 0 to azure with ASP.Net Core 5 and docker");
            }); // Gera uma página html

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id}");
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");
            });
        }
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new EvolveDb.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                };
                evolve.Migrate();
            }
            catch(Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}


// Autorização Token - JWT
// só se faz a autenticação uma vez (usuário e senha), manda-se o post request por end point de login, esse vai validar se o usuário tem a permissão e devolve o token se tiver a permissão, o client armazena esse token em uma variável do header chamada autorization, sempre que fizer as requisições, tem que mandar esse cabeçalho, caso contrário receberá um resposta de unauthorized