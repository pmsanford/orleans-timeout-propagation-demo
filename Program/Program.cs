using System;
using Lib;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine($"Server or client?");
            }
            if (args[0].Equals("client", StringComparison.InvariantCultureIgnoreCase))
            {
                var waitTime = 90000;
                var delayTime = 80000;
                var multiLevel = true;
                if (args.Length > 1)
                {
                    multiLevel = args[1].Equals("multi", StringComparison.InvariantCultureIgnoreCase);
                }
                if (args.Length > 2)
                {
                    int.TryParse(args[2], out waitTime);
                }
                if (args.Length > 3)
                {
                    int.TryParse(args[3], out delayTime);
                }
                Console.WriteLine($"Preparing to wait for {waitTime} ms on a {delayTime} grain.");
                var client = new Client();
                if (multiLevel)
                {
                    client.RunCascade(waitTime, delayTime).Wait();
                }
                else
                {
                    client.RunSingle(waitTime, delayTime).Wait();
                }
                Console.WriteLine($"Complete.");
            }
            else
            {
                Console.WriteLine($"Preparing silo.");
                var silo = new Silo();
                silo.StartSilo().Wait();
            }
        }
    }
}
