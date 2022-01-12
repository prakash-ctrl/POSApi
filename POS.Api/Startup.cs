using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POS.Api.Cache;
using POS.Api.Extensions;
using POS.Api.Swagger;
using POS.Api.Token;
using POS.BusinessLogic;
using POS.DataContext;
using POS.Utility;
using System.Collections.Generic;
using System.Linq;

namespace POS.Api
{
    public class Startup
    {
        #region Start Up
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IntializeApplicationConstants(configuration);
            MsSqlConnect.connectionString = ApplicationConstants.POS_CONNECTION_STRING;
            
            
        }

      
        #endregion
        public IConfiguration Configuration { get; }
        
        public IHostEnvironment environment { get; set; }
        #region Configure Services
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MapperProfile.MapperProfile));
            services.AddCors(options =>
            {
                options.AddPolicy(ApplicationConstants.DEFAULT_CORS_POLICY,
                    builder => builder.WithOrigins(ApplicationConstants.Origins.ToArray<string>())
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            AddDependecy(services);
            ConfigureSwagger(services);

        }
        #endregion
        #region Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureLogger(env, app);
            ConfigureCache(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(ApplicationConstants.DEFAULT_CORS_POLICY);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                if (env.IsDevelopment())
                {
                    options.SwaggerEndpoint("/swagger/V1/swagger.json", "POS Api");
                }
                else
                {
                    options.SwaggerEndpoint($"/{ApplicationConstants.APP_NAME}/swagger/V1/swagger.json", "POS Api");
                }
            });
        }
        #endregion
        #region Custom Configuration
        #region Initializing the Application Constants
        /// <summary>
        /// Initializing values for Application Constants
        /// </summary>
        /// <param name="configuration"></param>
        public void IntializeApplicationConstants(IConfiguration configuration)
        {
            var appSettings = Configuration.GetSection("appSettings");
            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            ApplicationConstants.POS_CONNECTION_STRING = connectionStrings.GetValue<string>("POS_DBCONNECT");
            ApplicationConstants.APP_NAME = appSettings.GetValue<string>("AppName");
            ApplicationConstants.SYMMETRIC_KEY = appSettings.GetValue<string>("SymmetricKey");
            ApplicationConstants.LOGGER_PATH = appSettings.GetValue<string>("LogPath");
            ApplicationConstants.TOKEN_EXPIRATION_HOURS = appSettings.GetValue<int>("TokenExpiry");
            ApplicationConstants.Origins = appSettings.GetValue<string>("Origins").Split<string>();
        }


        #endregion
        #region Depndency Injection Configuration

        public void AddDependecy(IServiceCollection services)
        {
            services.AddTransient<MsSqlConnect>();
            services.AddTransient<LoggerContext>();
            services.AddTransient<UserDataContext>();

            services.AddScoped<BaseBL>();
            services.AddScoped<LoggerBL>();
            services.AddScoped<Logger>();
            services.AddScoped<UserBL>();
            services.AddScoped<TokenManager>();
        }


        #endregion
        #region Swagger Configuration

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options => {

                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title="POS Api",
                    Version="V1",
                    Description="Api for Point Of Sale"
                });

                options.OperationFilter<SwaggerCustomParameter>();
            
            });
        }


        #endregion

        #region Logger Configuration
        public void ConfigureLogger(IWebHostEnvironment env, IApplicationBuilder app)
        {

            if (string.IsNullOrEmpty(ApplicationConstants.LOGGER_PATH))
                ApplicationConstants.LOGGER_PATH = env.ContentRootPath;
            Logger.LoggerPath = $"{ApplicationConstants.LOGGER_PATH}\\ApplicationLogs";
            LoggerExtensions.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(),
                app.ApplicationServices.GetService<LoggerBL>());
        }

        #endregion

        #region Configure Cahe

        public void ConfigureCache(IApplicationBuilder app)
        {
            CacheReference.ConfigureIMemoryCache(app.ApplicationServices.GetRequiredService<IMemoryCache>(),
                app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            CacheReference.InjectService(app.ApplicationServices.GetService<LoggerBL>());
            CacheReference.InitializeCache();

        }

        #endregion


        #endregion
    }
}
