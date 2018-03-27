using System.Threading.Tasks;
using Orleans;

namespace Lib
{
    public interface ISubGrain : IGrainWithIntegerKey
    {
        Task DoSomethingElse();
    }
}