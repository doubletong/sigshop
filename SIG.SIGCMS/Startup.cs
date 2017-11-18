﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using SIG.Infrastructure.Helper;
using SIG.Services;
using SIG.Services.Log;
using SIG.Services.Menus;
using Swashbuckle.AspNetCore.Swagger;

namespace SIG.SIGCMS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SIGDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("SIG.SIGCMS")), ServiceLifetime.Singleton, ServiceLifetime.Singleton)
                .AddUnitOfWork<SIGDbContext>();

            services.AddAuthentication(
            //    options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //}
            ).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Account/LogIn");
                options.LogoutPath = new PathString("/Account/LogOff");
                options.AccessDeniedPath = new PathString("/Errors/AccessDenied");
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["TokenOptions:Issuer"],
                        ValidAudience = Configuration["TokenOptions:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:Key"])),
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement("/errors/accessdenied")));
            });
            //
            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Shayne Boyer", Email = "", Url = "https://twitter.com/spboyer" },
                    License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });
            
                c.SwaggerDoc("v2", new Info { Title = "My API - V2", Version = "v2" });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "SIG.SIGCMS.xml");
                c.IncludeXmlComments(xmlPath);

            });

         

            //services.AddSingleton<DbContext, SIGDbContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork<SIGDbContext>>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            //services.AddScoped<DbContext, SIGDbContext>();
            //services.AddScoped<IUnitOfWork, UnitOfWork<SIGDbContext>>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddMemoryCache();
            services.AddMvc();
            services.AddAutoMapper();

            // Add application services. 依赖注入
            //services.AddTransient<DbContext, SIGDbContext>();
            //services.AddTransient<IUnitOfWork, UnitOfWork<SIGDbContext>>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IMenuServices, MenuServices>();
            services.AddTransient<IMenuCategoryServices, MenuCategoryServices>();
            services.AddTransient<ILogServices, LogServices>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthorization(options => options.AddPolicy("Trusted", policy => policy.RequireClaim("Employee", "Mosalla")));

            services.AddOptions();
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
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

            /*https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio */
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auto Beauty API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API - V2");
             
            });

        }
    }
}
