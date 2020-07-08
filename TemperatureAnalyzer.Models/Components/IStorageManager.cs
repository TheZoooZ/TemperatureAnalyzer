using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Models;

namespace TemperatureAnalyzer.Core.Components
{
    public interface IStorageManager
    {
        Task StoreDataAsync(string rawData);
        Task<IEnumerable<TemperatureData>> GetDataAsync();
    }
}