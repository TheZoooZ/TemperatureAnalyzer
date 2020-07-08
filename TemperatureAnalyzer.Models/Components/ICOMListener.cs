using System.Threading;
using System.Threading.Tasks;

namespace TemperatureAnalyzer.Core.Components
{
    public interface ICOMListener
    {
        Task ListenPortAsync(CancellationToken cancellationToken);
    }
}