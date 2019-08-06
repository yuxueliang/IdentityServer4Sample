using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityServer4Mvc.Services;
using IdentityServer4Mvc.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServer4Mvc.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4Mvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options=> 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddIdentityServer()
                  .AddDeveloperSigningCredential()
                  .AddInMemoryApiResources(Config.GetApiResources())
                  .AddInMemoryIdentityResources(Config.GetIdentityResources())
                  .AddInMemoryClients(Config.GetClients())
                  .AddAspNetIdentity<ApplicationUser>();
                  //.AddTestUsers(Config.GetTestUsers());

            services.AddScoped<ConsentService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseIdentityServer();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
