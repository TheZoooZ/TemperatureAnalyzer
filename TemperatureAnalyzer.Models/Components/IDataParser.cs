using System.Collections.Generic;
using TemperatureAnalyzer.Core.Models;

namespace TemperatureAnalyzer.Core.Components
{
    public interface IDataParser
    {
        IEnumerable<TemperatureData> ParseRawData(string rawData);
    }
}