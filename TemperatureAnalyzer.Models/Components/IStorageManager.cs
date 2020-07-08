using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Models;

namespace TemperatureAnalyzer.Core.Components
{
    public interface IStorageManager
    {
        Task<TemperatureData> GetLatestDataAsync();
        Task<TemperatureData> GetOldestDataAsync();
        Task StoreDataAsync(TemperatureData data);
        Task RemoveDataAsync(TemperatureData data);
        Task<IEnumerable<TemperatureData>> GetDataAsync();
    }
}