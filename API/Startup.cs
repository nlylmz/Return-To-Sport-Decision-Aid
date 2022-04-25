using ReturnToSport.Repo;
using ReturnToSport.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ReturnToSport.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
            });
            services.AddControllers();
            services.AddDbContext<MainContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DevelopmentConnection");
                options.SchemaName = "dbo";
                options.TableName = "Cache";
            });

            services.AddSession();

            services.AddScoped(typeof(IMainRepository<>), typeof(MainRepository<>));
            services.AddTransient<IMainServis, MainServis>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Template API",
                    Description = "A template API for web applications."
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                if (env.IsDevelopment())
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API");
                else
                    c.SwaggerEndpoint("/API/swagger/v1/swagger.json", "Template API");
                c.RoutePrefix = string.Empty; // "http://api.tp.gov.tr/kbysapi/" olarak çalışması için.
            });

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
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
