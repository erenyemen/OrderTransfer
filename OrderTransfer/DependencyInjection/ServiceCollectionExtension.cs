using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.ChannelAdvisor;
using OrderTransfer.Helpers.TPLCentral;
using OrderTransfer.Models.Settings;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITPLCentralSettings>(s => configuration.GetSection("TPLCentral").Get<TPLCentralSettings>());
            services.AddSingleton<IChannelAdvisorSettings>(s => configuration.GetSection("ChannelAdvisor").Get<ChannelAdvisorSettings>());

            return services;
        }

        public static IServiceCollection AddChannelAdvisorApi(this IServiceCollection services)
        {
            var requestContextService = services.GetServiceCollectionObject<IChannelAdvisorSettings>();
            var logger = services.GetServiceCollectionObject<ILogger<ChannelAdvisorApiHelper>>();

            services.AddSingleton<IChannelAdvisorApiHelper>(s => new ChannelAdvisorApiHelper(logger, requestContextService));

            return services;
        }

        public static IServiceCollection AddTplCentralApi(this IServiceCollection services)
        {
            var apiService = services.GetServiceCollectionObject<ITPLCentralSettings>();
            var logger = services.GetServiceCollectionObject<ILogger<TPLCentralApiHelper>>();

            services.AddSingleton<ITPLCentralApiHelper>(s => new TPLCentralApiHelper(logger, apiService));

            return services;
        }

        public static T GetServiceCollectionObject<T>(this IServiceCollection services)
        {
            return (T)services.BuildServiceProvider().GetService(typeof(T));
        }

        public static IServiceCollection AddSeriLogOrderTransfer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"))
                  .AddSerilog(new LoggerConfiguration().WriteTo.File("serilog.txt").CreateLogger())
                  .AddConsole();
#if DEBUG
                builder.AddDebug();
#endif
            });

            return services;
        }
    }
}