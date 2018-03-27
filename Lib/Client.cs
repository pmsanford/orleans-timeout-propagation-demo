using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace Lib
{
    public class Client
    {
        private IClusterClient GetClient(int timeInMs)
        {
            var builder = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ITopGrain).Assembly).WithReferences())
                .UseLocalhostClustering()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                .Configure<ClientMessagingOptions>(opts =>
                {
                    opts.ResponseTimeout = TimeSpan.FromMilliseconds(timeInMs);
                })
                .Configure<SiloMessagingOptions>(opts =>
                {
                    opts.ResponseTimeout = TimeSpan.FromMilliseconds(timeInMs);
                })
                .Configure<MessagingOptions>(opts =>
                {
                    opts.ResponseTimeout = TimeSpan.FromMilliseconds(timeInMs);
                });
            return builder.Build();
        }
        public async Task RunSingle(int timeInMs, int delayInMs)
        {
            using (var client = GetClient(timeInMs))
            {
                await client.Connect();
                var grain = client.GetGrain<ISubGrain>(delayInMs);
                var promise = grain.DoSomethingElse();
                promise.Wait();
                if (promise.IsFaulted)
                {
                    Console.WriteLine($"Exception: {promise.Exception}");
                }
                else
                {
                    Console.WriteLine("Success");
                }
            }
        }
        public async Task RunCascade(int timeInMs, int delayInMs)
        {
            using (var client = GetClient(timeInMs))
            {
                await client.Connect();
                var grain = client.GetGrain<ITopGrain>(delayInMs);
                var promise = grain.DoThingAsync();
                promise.Wait();
                if (promise.IsFaulted)
                {
                    Console.WriteLine($"Exception: {promise.Exception}");
                }
                else
                {
                    Console.WriteLine("Success");
                }
            }
        }
    }
}