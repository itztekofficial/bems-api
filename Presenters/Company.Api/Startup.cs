using Company.Api;
using Company.Api.Extensions;
using Core;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middlewares;
using Serilog;
using System;
using System.IO;
using System.IO.Compression;

namespace Company
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
        ///
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Injecting AppSettings
            AppSettings appSettings = new();
            Configuration.GetSection("AppSettings").Bind(appSettings);

            //services.Configure<ElasticConfiguration>(options => Configuration.GetSection(ElasticConfiguration.Name).Bind(options));
            //services.Configure<CacheConfiguration>(options => Configuration.GetSection(CacheConfiguration.Name).Bind(options));

            //Add Authentication
            //byte[] signingKey = Encoding.UTF8.GetBytes(appSettings.Secret);
            services.AddAuthentication(appSettings);

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

            //Project specific injecting services.
            string connString = Configuration.GetConnectionString("DBConnection");
            services.AddCompanyServices(connString);

            services.AddControllers();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Clear();
                options.DefaultRequestCulture = new RequestCulture("en-IN");
                options.SetDefaultCulture("en-IN");
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="lifetime"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {

            }
            else
            {
                app.UseHsts();
            }

            app.UseSwaggerDocumentation();
            app.UseDeveloperExceptionPage();

            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);

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

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("X-Frame-Options");
                context.Response.Headers.Add("X-Frame-Options", "AllowAll");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void OnStarted()
        {
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "OnStarted");
            }
        }

        private static void OnStopping()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "OnStopping");
            }
        }
    }
}