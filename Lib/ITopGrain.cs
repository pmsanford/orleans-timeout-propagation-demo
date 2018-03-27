using System.Threading.Tasks;
using Orleans;

namespace Lib
{
    public interface ITopGrain : IGrainWithIntegerKey
    {
        Task DoThingAsync();
    }
}