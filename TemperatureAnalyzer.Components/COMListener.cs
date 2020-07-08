using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Components;
using TemperatureAnalyzer.Core.Logging;
using TemperatureAnalyzer.Core.Settings;

namespace TemperatureAnalyzer.Components
{
    public class COMListener : ICOMListener
    {
        private readonly ICOMSettings comSettings;
        private readonly IStorageManager storageManager;
        private readonly IDataParser dataParser;
        private readonly ILogger<COMListener> logger;

        public COMListener(ICOMSettings comSettings, ILogger<COMListener> logger, IStorageManager storageManager, IDataParser dataParser)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.comSettings = comSettings ?? throw new ArgumentNullException(nameof(comSettings));
            this.storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
            this.dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        }

        public async Task ListenPortAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(async () =>
            {
                if (TryOpenSerialPort(out var serialPort))
                {
                    logger.LogInformation(Logs.PortListeningStarted);
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var parsedData = dataParser.ParseRawData(serialPort.ReadLine());
                        await storageManager.StoreDataAsync(parsedData);
                        await Task.Delay(comSettings.ListeningDelay);
                    }
                }
                else
                    logger.LogError(Logs.SerialPortClosed);

                logger.LogInformation(Logs.PortListeningStopped);
            });
        }

        private bool TryOpenSerialPort(out SerialPort serialPort)
        {
            serialPort = new SerialPort()
            {
                BaudRate = comSettings.BoudRate,
                PortName = comSettings.PortName
            };
            serialPort.Open();

            return serialPort.IsOpen;
        }
    }
}