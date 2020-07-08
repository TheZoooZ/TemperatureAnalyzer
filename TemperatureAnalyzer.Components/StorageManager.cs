using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Components;
using TemperatureAnalyzer.Core.Models;
using TemperatureAnalyzer.Core.Settings;

namespace TemperatureAnalyzer.Components
{
    public class StorageManager : IStorageManager
    {
        private readonly IStorageSettings storageSettings;
        private readonly IDataParser dataParser;

        public StorageManager(IStorageSettings storageSettings, IDataParser dataParser)
        {
            this.storageSettings = storageSettings ?? throw new ArgumentNullException(nameof(storageSettings));
            this.dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        }

        public async Task<IEnumerable<TemperatureData>> GetDataAsync()
        {
            using var sourceStream = new FileStream(
                path: storageSettings.StorageFilePath,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.Read,
                bufferSize: storageSettings.BufferSize,
                useAsync: true);

            var rawOutput = await ReadContentFromFile(sourceStream);

            return dataParser.ParseRawData(rawOutput);
        }

        public async Task StoreDataAsync(string rawData)
        {
            var encodedText = Encoding.Unicode.GetBytes(rawData);

            using var sourceStream = new FileStream(
                path: storageSettings.StorageFilePath,
                mode: FileMode.Append,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: storageSettings.BufferSize,
                useAsync: true);

            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }

        private static async Task<string> ReadContentFromFile(FileStream sourceStream)
        {
            StringBuilder sb = new StringBuilder();

            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}