namespace TemperatureAnalyzer.Core.Settings
{
    public interface ICOMSettings
    {
        int BoudRate { get; }
        string PortName { get; }
        int ListeningDelay { get; }
    }
}