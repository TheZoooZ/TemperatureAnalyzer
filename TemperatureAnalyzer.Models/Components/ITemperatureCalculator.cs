using System.Threading.Tasks;

namespace TemperatureAnalyzer.Core.Components
{
    public interface ITemperatureCalculator
    {
        Task<decimal> GetLatestTemperature();
        Task<decimal> GetMaxTemperature();
        Task<decimal> GetMinTemperature();
        Task<decimal> GetAvgTemperature();
    }
}