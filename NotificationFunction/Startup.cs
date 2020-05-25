using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using NG.BLL.Services.Interfaces;
using NG.BLL.Services;
using NG.BLL.Providers;
using NG.BLL.Providers.Interfaces;
using NG.BLL.Configuration;
using Microsoft.Extensions.Configuration;
using NG.BLL.ApiClient;
using NG.BLL.Constants;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Azure.WebJobs.Host.Bindings;

[assembly: FunctionsStartup(typeof(NotificationFunction.Startup))]

namespace NotificationFunction
{
    public class Startup : FunctionsStartup
    {
        //private IConfigurationRoot _functionConfig = null;

        //private IConfigurationRoot FunctionConfig(string appDir) =>
        //_functionConfig ??= new ConfigurationBuilder()
        //    .AddJsonFile(Path.Combine(appDir, "appsettings.json"), optional: true, reloadOnChange: true).Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddOptions<ProvidersSettings>()
            // .Configure<IOptions<ExecutionContextOptions>>((mlSettings, exeContext) =>
            //     FunctionConfig(exeContext.Value.AppDirectory).GetSection("ProvidersSettings").Bind(mlSettings));

            //builder.Services.AddOptions<AppSettings>()
            // .Configure<IOptions<ExecutionContextOptions>>((mlSettings, exeContext) =>
            //     FunctionConfig(exeContext.Value.AppDirectory).GetSection("AppSettings").Bind(mlSettings));

            var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false)
                   .AddEnvironmentVariables()
                   .Build();

            //builder.Services.AddOptions<ProvidersSettings>().Configure<IConfiguration>((settings, configuration) => { configuration.GetSection("ProvidersSettings").Bind(settings); });
            //builder.Services.AddOptions<AppSettings>().Configure<IConfiguration>((settings, configuration) => { config.GetSection("AppSettings").Bind(settings); });
            builder.Services.Configure<ProvidersSettings>(config.GetSection("ProvidersSettings"));
            builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ICallerProvider, TwilioProvider>();
            builder.Services.AddScoped<ICallerProvider, IFTTTProvider>();
            builder.Services.AddOptions();

            var configuration = config.GetSection("ProvidersSettings").Get<ProvidersSettings>();
            builder.Services.AddHttpClient<IFTTTClient>(x => { x.BaseAddress = new Uri(configuration[ProvidersConstants.IFTTT].Url); });

            //var serviceProvider = builder.Services.BuildServiceProvider();
            //var configurationRoot = serviceProvider.GetService<IConfiguration>();

            //var app = configurationRoot.GetSection(nameof(AppSettings)).Get<AppSettings>();
            //var configuration = configurationRoot.GetSection(nameof(ProvidersSettings)).Get<ProvidersSettings>();

            //var configuration = _functionConfig.GetSection(nameof(ProvidersSettings)).Get<ProvidersSettings>();
            //builder.Services.AddHttpClient<IFTTTClient>(x => { x.BaseAddress = new Uri(configuration[ProvidersConstants.IFTTT].Url); });
        }
    }
}
