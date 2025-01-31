using Moq;
using System.Collections.Generic;
using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;
using Weather.Domain.Enums;
using Weather.Service.Weather;
using Xunit;

namespace Weather.UnitTest.Service
{
    public class WeatherServiceTests
    {
        private readonly WeatherService _service;
        private readonly Mock<IOpenWeatherMapWorker> _openWeatherMapWorker;

        public WeatherServiceTests()
        {
            _openWeatherMapWorker = new Mock<IOpenWeatherMapWorker>();
            _service = new WeatherService(_openWeatherMapWorker.Object);            
        }

        [Fact]
        public void GetWeatherByCityName_ShouldReturnSuccess_WhenApiReturnsValidData()
        {
            // Arrange
            var expectedWeather = new OpenWeatherMapDto 
            { 
                Main = new MainDto(),
                Coordinate = new CoordinateDto(),
                Weather = new List<WeatherDetailDto> { new WeatherDetailDto { Main = "Clear" } },
                Wind = new WindDto { Speed = 8 }
            };
            _openWeatherMapWorker.Setup(ws => ws.GetOpenWeatherMapData("London"))
                              .Returns(OperationResult<OpenWeatherMapDto>.Success(expectedWeather));


            // Act
            var result = _service.GetWeatherByCityName("London");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);
            Assert.Equal(8, result.Value.Wind);
            Assert.Equal("Clear", result.Value.SkyCondition);
        }

        [Fact]
        public void GetWeatherByCityName_ShouldReturnSuccess_WithCorrectTemperatureConversion()
        {
            // Arrange
            var expectedWeather = new OpenWeatherMapDto
            {
                Main = new MainDto { Temperature = 279.15M },
                Coordinate = new CoordinateDto(),
                Weather = new List<WeatherDetailDto> { new WeatherDetailDto() },
                Wind = new WindDto()
            };
            _openWeatherMapWorker.Setup(ws => ws.GetOpenWeatherMapData("London"))
                              .Returns(OperationResult<OpenWeatherMapDto>.Success(expectedWeather));


            // Act
            var result = _service.GetWeatherByCityName("London");

            // Assert
            Assert.Equal(279.15M, result.Value.TemperatureFahrenheit);
            Assert.Equal(137.31M, result.Value.TemperatureCelcius);
        }

        [Fact]
        public void GetWeatherByCityName_ShouldReturnFail_WhenApiReturnsError()
        {
            // Arrange
            _openWeatherMapWorker.Setup(ws => ws.GetOpenWeatherMapData("InvalidCity"))
                              .Returns(OperationResult<OpenWeatherMapDto>.Fail(OperationResultType.InvalidArguments, "City not found"));

            // Act
            var result = _service.GetWeatherByCityName("InvalidCity");

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsResultOk);
            Assert.Equal(OperationResultType.InvalidArguments, result.ResultType);
            Assert.Equal("City not found", result.Failure.Reason);
        }
    }
}
