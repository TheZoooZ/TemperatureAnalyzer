using TemperatureAnalyzer.Core.Models;

namespace TemperatureAnalyzer.Core.Components
{
    public interface IDataParser
    {
        TemperatureData ParseRawData(string rawData);
    }
}
