using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Lib
{
    public class TopGrain : Grain, ITopGrain
    {
        private readonly ILoggerFactory loggerFactory;
        private ILogger logger;

        public TopGrain(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger("TopLogger");
        }

        public async Task DoThingAsync()
        {
            var time = this.GetPrimaryKeyLong();
            var timeSlice = (int)time / 4;
            var subGrain = GrainFactory.GetGrain<ISubGrain>(timeSlice * 2);
            await Delay(timeSlice);
            await subGrain.DoSomethingElse();
            await Delay(timeSlice);
        }

        private async Task Delay(int time)
        {
            logger.LogWarning($"Preparing to delay {time} ms");
            await Task.Delay(time);
            logger.LogWarning("Ending delay.");
        }
    }
}