using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    services.AddConfigurations(configuration);
                    services.AddChannelAdvisorApi();
                    services.AddTplCentralApi();
                    services.AddHostedService<Worker>();
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
