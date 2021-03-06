﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using WebApi.Interfaces;
using WebApi.Repository;

namespace WebApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        public static IApplicationEnvironment _appEnvironment;
        public static dynamic AppSettings;
        public Startup(
            IHostingEnvironment env, 
            IApplicationEnvironment appEnv)
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            

        }
        public void ConfigureServices(
            IServiceCollection services)
        {

            
            services.AddMvc();
            services.AddSingleton<ITodo, TodoRepository>();
            services.AddSingleton<IMenu, MenuRepository>();
            services.AddSingleton<ILabel, LabelRepository>();
            services.AddSingleton<IForm, FormRepository>();
            services.AddSingleton<IDataSource, DataSourceRepository>();
            services.AddSingleton<IMedia, MediaRepository>();
            services.AddSingleton<IAppConfig, AppConfigRepository>();
            services.AddSingleton<IPage, PageRepository>();
            services.AddSingleton<IStatic, StaticRepository>();
            services.AddSingleton<IImage, ImageRepository>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IApplicationEnvironment appEnvironment, 
            IAppConfig webappconfig)
        {

            _appEnvironment = appEnvironment;
            AppSettings = webappconfig.Get();
            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            app.UseMvc();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
