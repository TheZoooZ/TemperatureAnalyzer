using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureAnalyzer.Core.Components;
using TemperatureAnalyzer.Core.Models;

namespace TemperatureAnalyzer.Components.Tests.Unit
{
    [TestFixture]
    public class TemperatureCalculatorTests
    {
        private Mock<IStorageManager> storageManager;

        private readonly DateTime date = DateTime.UtcNow;

        [SetUp]
        public void BeforeEach()
        {
            storageManager = new Mock<IStorageManager>();
        }

        [Test]
        public async Task GetAvgTemperatureReturnsZeroIfNoDataExist()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>());
            var calculator = CreateComponent();

            var result = await calculator.GetAvgTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetAvgTemperatureReturnsZeroIfCollectionIsNull()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync((List<TemperatureData>)null);
            var calculator = CreateComponent();

            var result = await calculator.GetAvgTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetAvgTemperatureReturnsAvgValue()
        {
            const decimal expectedTemperature = 75;
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>
            {
                new TemperatureData
                {
                    Date = date.AddMinutes(1),
                    Value = 100
                },
                new TemperatureData
                {
                    Date = date.AddMinutes(2),
                    Value = 50
                }
            });
            var calculator = CreateComponent();

            var result = await calculator.GetAvgTemperature();

            result.Should().Be(expectedTemperature);
        }

        [Test]
        public async Task GetLatestTemperatureReturnsLastNoticedTemperature()
        {
            const decimal expectedTemperature = 75;
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>
            {
                new TemperatureData
                {
                    Date = date.AddMinutes(1),
                    Value = 100
                },
                new TemperatureData
                {
                    Date = date.AddMinutes(2),
                    Value = expectedTemperature
                }
            });
            var calculator = CreateComponent();

            var result = await calculator.GetLatestTemperature();

            result.Should().Be(expectedTemperature);
        }

        [Test]
        public async Task GetLatestTemperatureReturnsZeroIfNoDataExist()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>());
            var calculator = CreateComponent();

            var result = await calculator.GetLatestTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetLatestTemperatureReturnZeroIfCollectionIsNull()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync((List<TemperatureData>)null);
            var calculator = CreateComponent();

            var result = await calculator.GetLatestTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetMaxTemperatureReturnsMaxTemperature()
        {
            const decimal expectedTemperature = 75;
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>
            {
                new TemperatureData
                {
                    Date = date.AddMinutes(1),
                    Value = expectedTemperature
                },
                new TemperatureData
                {
                    Date = date.AddMinutes(2),
                    Value = 30
                }
            });
            var calculator = CreateComponent();

            var result = await calculator.GetMaxTemperature();

            result.Should().Be(expectedTemperature);
        }

        [Test]
        public async Task GetMaxTemperatureReturnsZeroIfNoDataExist()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>());
            var calculator = CreateComponent();

            var result = await calculator.GetMaxTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetMaxTemperatureReturnsZeroIfCollectionIsNull()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync((List<TemperatureData>)null);
            var calculator = CreateComponent();

            var result = await calculator.GetMaxTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetMinTemperatureReturnsMaxTemperature()
        {
            const decimal expectedTemperature = 75;
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>
            {
                new TemperatureData
                {
                    Date = date.AddMinutes(1),
                    Value = 100
                },
                new TemperatureData
                {
                    Date = date.AddMinutes(2),
                    Value = expectedTemperature
                }
            });
            var calculator = CreateComponent();

            var result = await calculator.GetMinTemperature();

            result.Should().Be(expectedTemperature);
        }

        [Test]
        public async Task GetMinTemperatureReturnsZeroIfNoDataExist()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync(new List<TemperatureData>());
            var calculator = CreateComponent();

            var result = await calculator.GetMinTemperature();

            result.Should().Be(0);
        }

        [Test]
        public async Task GetMinTemperatureReturnsZeroIfCollectionIsNull()
        {
            storageManager.Setup(x => x.GetDataAsync()).ReturnsAsync((List<TemperatureData>)null);
            var calculator = CreateComponent();

            var result = await calculator.GetMinTemperature();

            result.Should().Be(0);
        }

        private ITemperatureCalculator CreateComponent()
            => new TemperatureCalculator(storageManager.Object);
    }
}