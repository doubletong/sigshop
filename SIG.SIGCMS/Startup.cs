using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using SIG.Data.Entity;
using SIG.Repository;
using SIG.Services.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SIG.Infrastructure.Helper;
using SIG.Services;
using SIG.Services.Log;
using SIG.Services.Menus;

namespace SIG.SIGCMS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SIGDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("SIG.SIGCMS")))
                .AddUnitOfWork<SIGDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Account/LogIn");
                options.LogoutPath = new PathString("/Account/LogOff");
                options.AccessDeniedPath = new PathString("/Error/Login");
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SigAuth",
                    policy => policy.Requirements.Add(new EnterBuildingRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, BadgeEntryHandler>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddMemoryCache();
            services.AddMvc();
            services.AddAutoMapper();
     
            // Add application services. 依赖注入
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IMenuServices, MenuServices>();
            services.AddTransient<IMenuCategoryServices, MenuCategoryServices>();
            services.AddTransient<ILogServices, LogServices>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
           
            ////add NLog.Web
            app.AddNLogWeb();
            //needed for non-NETSTANDARD platforms: configure nlog.config in your project root. NB: you need NLog.Web.AspNetCore package for this.         
            env.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");

            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境异常处理
                //MVC 页面跳转
                app.UseExceptionHandler("/Error");

                //webapi 提示错误
                //app.UseExceptionHandler(appBuilder =>
                //{
                //    appBuilder.Run(async context =>
                //    {
                //        var feature = context.Features.Get<IExceptionHandlerFeature>();
                //        var exception = feature.Error;
                //        await context.Response.WriteAsync(
                //            $"<b>Oops!</b> {exception.Message}");
                //    });
                //});
            }
            app.UseAuthentication();

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.html");
            //app.UseDefaultFiles(options);
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
