using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SingleDebtControl.Api.Infra;

namespace SingleDebtControl.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region

            services.AddCors(options => {
                options.AddPolicy("SiteCorsPolicy", builder =>
                builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
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
                            Name = "Cloudmed",
                            Email = "lucas.dcorrea1@gmail.com",
                        }
                    });
            });

            services.AddControllers();

            services.Resolve();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SingleDebitControl v1.0");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
