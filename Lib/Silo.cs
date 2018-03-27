using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Lib
{
    public class Silo
    {
        private readonly ManualResetEventSlim mre = new ManualResetEventSlim();
        public Task StopSilo()
        {
            mre.Set();
            return Task.CompletedTask;
        }
        public async Task StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<EndpointOptions>(opts => opts.AdvertisedIPAddress = IPAddress.Loopback)
                .AddMemoryGrainStorageAsDefault()
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TopGrain).Assembly).WithReferences())
                .ConfigureLogging(logger =>
                {
                    logger.AddConsole();
                    logger.SetMinimumLevel(LogLevel.Warning);
                })
                .ConfigureServices(svc =>
                {
                });

            using (var silo = builder.Build())
            {
                await silo.StartAsync();
                Console.WriteLine($"Silo started.");
                mre.Wait();
                await silo.StopAsync();
                Console.WriteLine($"Silo stopped.");
            }
        }
    }
}
