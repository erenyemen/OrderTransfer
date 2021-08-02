using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OrderTransfer
{
    public class Program
    {
        static IConfiguration configuration;

        public static void Main(string[] args)
        {
            configuration = GetConfiguration(args);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddConfigurations(configuration)
                            .AddSeriLogOrderTransfer(configuration)
                            .AddChannelAdvisorApi()
                            .AddTplCentralApi()
                            .AddHostedService<OrderDeliveryWorker>()
                            .AddHostedService<OrderFulfillmentWorker>();
                });

        private static IConfiguration GetConfiguration(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configurationBuilder.AddCommandLine(args);

            return configurationBuilder.Build();
        }
    }
}
