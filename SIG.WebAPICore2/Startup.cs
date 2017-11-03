using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using SIG.Data.Entity;
using SIG.Repository;
using Swashbuckle.AspNetCore.Swagger;
using SIG.Services.Identity;

namespace SIG.WebAPICore2
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
                        b => b.MigrationsAssembly("SIG.WebAPICore2")))
                .AddUnitOfWork<SIGDbContext>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.LoginPath = new PathString("/Account/LogIn");
            //    options.LogoutPath = new PathString("/Account/LogOff");
            //    options.AccessDeniedPath = new PathString("/Error/Forbidden");
            //});

            services.AddAuthentication("FiverSecurityScheme")
                   .AddCookie("FiverSecurityScheme", options =>
                   {
                       options.AccessDeniedPath = new PathString("/Security/Access");
                       options.Cookie = new CookieBuilder
                       {
                            //Domain = "",
                            HttpOnly = true,
                           Name = ".Fiver.Security.Cookie",
                           Path = "/",
                           SameSite = SameSiteMode.Lax,
                           SecurePolicy = CookieSecurePolicy.SameAsRequest
                       };
                       options.Events = new CookieAuthenticationEvents
                       {
                           OnSignedIn = context =>
                           {
                               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                 "OnSignedIn", context.Principal.Identity.Name);
                               return Task.CompletedTask;
                           },
                           OnSigningOut = context =>
                           {
                               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                 "OnSigningOut", context.HttpContext.User.Identity.Name);
                               return Task.CompletedTask;
                           },
                           OnValidatePrincipal = context =>
                           {
                               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                                 "OnValidatePrincipal", context.Principal.Identity.Name);
                               return Task.CompletedTask;
                           }
                       };
                        //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                        options.LoginPath = new PathString("/Security/Login");
                       options.ReturnUrlParameter = "RequestPath";
                       options.SlidingExpiration = true;
                   });

            //services.AddDistributedMemoryCache();
            //services.AddSession();

            // 不允许匿名访问
            services.AddMvc(options =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlDataContractSerializerFormatters();

            services.AddScoped<LogFilter>();
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Auto Beauty API", Version = "v1" });
                c.SwaggerDoc("v2", new Info { Title = "My API - V2", Version = "v2" });
            });

            // Add application services. 依赖注入
            services.AddTransient<IUserServices, UserServices>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
            //add NLog.Web
            app.AddNLogWeb();
            //needed for non-NETSTANDARD platforms: configure nlog.config in your project root. NB: you need NLog.Web.AspNetCore package for this.         
           // env.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error"); //MVC 页面跳转

                //webapi 提示错误
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerFeature>();
                        var exception = feature.Error;
                        await context.Response.WriteAsync(
                            $"<b>Oops!</b> {exception.Message}");
                    });
                });
            }

            //app.UseSession();
          
            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.html");
            //app.UseDefaultFiles(options);
            //app.UseStaticFiles();

            //MVC 页面错误跳转带状态
            //app.UseStatusCodePagesWithReExecute("/Errors/Index", "?statusCode={0}");
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
            app.UseAuthentication();

            /*https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio */
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auto Beauty API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API - V2");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Mvc disnot find anything!");
            });
        }
    }
}
