using Core;
using Core.Models;
using Login.Api;
using Login.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middlewares;
using Serilog;
using System;
using System.IO.Compression;
using System.Text;

namespace Login
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        //private DBUp.Contracts.IDatabaseMigration databaseMigration;

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        //margerpbll.Contracts.IDatabaseSQLBLL databaseSQLBLL;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = new();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.Configure<ElasticConfiguration>(options => Configuration.GetSection(ElasticConfiguration.Name).Bind(options));
            services.Configure<CacheConfiguration>(options => Configuration.GetSection(CacheConfiguration.Name).Bind(options));

            //Add Authentication
            byte[] signingKey = Encoding.UTF8.GetBytes(appSettings.Secret);
            services.AddAuthentication(signingKey);

            //Add Swagger Documentation
            services.AddSwaggerDocumentation();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.SetIsOriginAllowed(o => true)
                                 .AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddControllersWithViews();
            services.AddMediatR(typeof(Startup));

            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
            });

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddAutoMapper(typeof(Startup));

            //Global DI
            services.AddConnectCoreServices(Configuration);

            //Project Specific DI
            string connString = Configuration.GetConnectionString("DBConnection");
            services.AddLoginServices(connString);

            services.AddControllers();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Clear();
                options.DefaultRequestCulture = new RequestCulture("en-IN");
                options.SetDefaultCulture("en-IN");
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IServiceProvider serviceProvider)
        {
            //databaseMigration = (DBUp.Contracts.IDatabaseMigration)serviceProvider.GetService(typeof(DBUp.Contracts.IDatabaseMigration));

            if (env.IsDevelopment())
            {

            }
            else
            {
                app.UseHsts();
            }

            app.UseSwaggerDocumentation(env);
            app.UseDeveloperExceptionPage();

            //lifetime.ApplicationStarted.Register(OnStarted);
            //lifetime.ApplicationStopping.Register(OnStopping);

            app.UseRouting();
            app.UseCors("AllowAllHeaders");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseSerilogRequestLogging();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseNotFoundMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("X-Frame-Options");
                context.Response.Headers.Add("X-Frame-Options", "AllowAll");
                await next();
            });
        }

        //private void OnStarted()
        //{
        //    try
        //    {
        //        // We will Inject Service in Startup and Use DBUP-core
        //        databaseMigration.CreateDatabase();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex, "OnStarted");
        //    }
        //}

        //private static void OnStopping()
        //{
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex, "OnStopping");
        //    }
        //}
    }
}