using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SingleDebtControl.Api.Hangfire;
using SingleDebtControl.Api.Infra;
using System;

namespace SingleDebtControl.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ServiceProvider ServiceProvider { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Cors

            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());
            });

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SingleDebitControl",
                        Version = "v 1.0",
                        Description = "SingleDebitControl API",
                        Contact = new OpenApiContact
                        {
                            Name = "SingleDebitControl",
                            Email = "lucas.dcorrea1@gmail.com",
                            Url = new Uri("https://localhost/Debit")
                        }
                    });
            });

            services.AddControllers();

            services.Resolve();

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("SiteCorsPolicy");

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "SingleDebitControl v1.0");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();

            ServiceProvider.StartHangFireTasks(app);
        }
    }
}
