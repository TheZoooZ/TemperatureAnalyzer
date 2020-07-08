namespace TemperatureAnalyzer.Core.Settings
{
    public interface IStorageSettings
    {
        string StorageFilePath { get; }
        int BufferSize { get; }
    }
}