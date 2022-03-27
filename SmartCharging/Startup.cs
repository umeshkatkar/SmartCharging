using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartCharging.Business;
using SmartCharging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCharging
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
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Smart Charging Services", Version = "v2" }));
            
            //Fetching Connection string from APPSETTINGS.JSON  
            var ConnectionString = Configuration.GetConnectionString("DevConnection");
            services.AddSingleton(provider => Configuration);
            //services.AddSingleton<IConfiguration>(Configuration);
            //Entity Framework  
            services.AddDbContext<DBSmartChargingContext>(options => options.UseSqlServer(ConnectionString));
            services.AddControllers().AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = 
               Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );
            services.AddScoped<IChargingGroupBusiness, ChargingGroupBusiness>();
            services.AddScoped<IChargingStationBusiness, ChargingStationBusiness>();
            services.AddScoped<IChargingConnectorBusiness, ChargingConnectorBusiness>();
            services.AddScoped<IChargingGroupRepository, ChargingGroupRepository>();
            services.AddScoped<IChargingStationRepository, ChargingStationRepository>();
            services.AddScoped<IChargingConnectorRepository, ChargingConnectorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Smart Charging Services"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
