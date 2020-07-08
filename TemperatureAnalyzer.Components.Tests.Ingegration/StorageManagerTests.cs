using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TemperatureAnalyzer.Components.Tests.Integration.Helpers;
using TemperatureAnalyzer.Core.Components;

namespace TemperatureAnalyzer.Components.Tests.Ingegration
{
    [TestFixture]
    public class StorageManagerTests
    {
        private StorageSettingsMock storageSettings;
        private Mock<IDataParser> dataParser;

        public StorageManagerTests()
        {
            dataParser = new Mock<IDataParser>();
            storageSettings = new StorageSettingsMock();
        }

        [TearDown]
        public void AfterEach()
        {
            File.Delete(storageSettings.StorageFilePath);
        }

        [Test]
        public async Task StoreDataAsyncSavesDataToFile()
        {
            var rawData = "2020/21/04 13:45,45|2020/21/04 13:50";
            var component = CreateComponent();

            await component.StoreDataAsync(rawData);

            using var result = new StreamReader(storageSettings.StorageFilePath, Encoding.Unicode);
            result.ReadToEnd().Should().Be(rawData);
        }

        [Test]
        public async Task StoreDataAsyncAppendsDataToFile()
        {

        }

        private IStorageManager CreateComponent()
            => new StorageManager(storageSettings, dataParser.Object);
    }
}