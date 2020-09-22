using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using coreWeb.Models;
using Microsoft.OpenApi.Models;

namespace coreWeb
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddEntityFrameworkSqlite()
            //     .AddDbContext<coreWeb.Models.NewSystemContext>(item => item.UseSqlite($"Data Source={_appEnv.ApplicationBasePath}/newSystem.db");});
                
            services.AddDbContext<NewSystemContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:8080",
                                                        "http://1902.168.1.119:8080")
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod();
                                });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "New Core Web Application",
                    Description = "A simple Web Application to drive the Prodcuer / Consumer pattern .... ",
                    TermsOfService = new Uri("https://github.com/mckeepa/MessageHub"),
                    Contact = new OpenApiContact
                    {
                        Name = "Paul McKee",
                        Email = "Paul.McKee.aus@gmail.com",
                        Url = new Uri("https://twitter.com/paulmckee"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX in Github Repository",
                        Url = new Uri("https://github.com/mckeepa/MessageHub"),
                    }
                });
            });

            services.AddControllers();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Web App V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
