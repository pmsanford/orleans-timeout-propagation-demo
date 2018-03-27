using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Lib
{
    public class SubGrain : Grain, ISubGrain
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;

        public SubGrain(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger("SubLogger");
        }

        public async Task DoSomethingElse()
        {
            var time = this.GetPrimaryKeyLong();
            logger.LogWarning($"Preparing to delay {time} ms");
            await Task.Delay((int)time);
            logger.LogWarning("Ending wait.");
        }
    }
}