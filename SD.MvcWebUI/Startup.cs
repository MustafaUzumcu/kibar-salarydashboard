using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.Business.Abstract;
using SD.Business.Concrete;
using SD.DataAccess.Abstract;
using SD.DataAccess.Concrete.EntityFramework;
using SD.Entities;
using SD.MvcWebUI.Entities;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Business Modules
            services.AddScoped<ISystemParameterService, SystemParameterService>();
            services.AddScoped<ISystemParameterDal, EfSystemParameterDal>();

            // Features Services
            services.AddHttpContextAccessor();
            services.AddScoped<AlertMessageService>();

            // DbContext
            var connectionString = Configuration.GetConnectionString("SDConnection");
            services.AddDbContext<CustomIdentityDbContext>(o => o.UseSqlServer(connectionString));
            services.AddDbContext<SDContext>(o => o.UseSqlServer
                (connectionString, b=>b.MigrationsAssembly("SD.MvcWebUI"))
            );

            // Identity
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession();
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Identity
            app.UseAuthentication();

            // Client-Side library
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute
            (
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}
