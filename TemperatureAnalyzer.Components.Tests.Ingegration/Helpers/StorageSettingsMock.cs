using TemperatureAnalyzer.Core.Settings;

namespace TemperatureAnalyzer.Components.Tests.Integration.Helpers
{
    public class StorageSettingsMock : IStorageSettings
    {
        public string StorageFilePath => @"StorageMock.txt";
        public int BufferSize => 4096;
    }
}