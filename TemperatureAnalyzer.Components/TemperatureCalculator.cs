using System;
using System.Linq;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Components;

namespace TemperatureAnalyzer.Components
{
    public class TemperatureCalculator : ITemperatureCalculator
    {
        private readonly IStorageManager storageManager;

        public TemperatureCalculator(IStorageManager storageManager)
        {
            this.storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
        }

        public async Task<decimal> GetAvgTemperature()
        {
            var data = await storageManager.GetDataAsync();
            if (data != null && data.Any())
                return data.Average(data => data.Value);
            return 0;
        }

        public async Task<decimal> GetLatestTemperature()
        {
            var data = await storageManager.GetDataAsync();
            if (data != null && data.Any())
                return data.LastOrDefault().Value;
            return 0;
        }

        public async Task<decimal> GetMaxTemperature()
        {
            var data = await storageManager.GetDataAsync();
            if (data != null && data.Any())
                return data.Max(data => data.Value);
            return 0;
        }

        public async Task<decimal> GetMinTemperature()
        {
            var data = await storageManager.GetDataAsync();
            if (data != null && data.Any())
                return data.Min(data => data.Value);
            return 0;
        }
    }
}