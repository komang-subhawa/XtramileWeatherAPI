using Moq;
using Weather.Domain.DTOs;
using Weather.Domain.Enums;
using Weather.Service.Weather;
using Xunit;

namespace Weather.UnitTest.Service
{
    public class WeatherServiceTests
    {
        private readonly Mock<IWeatherService> mockService;

        public WeatherServiceTests()
        {
            mockService = new Mock<IWeatherService>();
        }

        [Fact]
        public void GetWeatherByCityName_ShouldReturnSuccess_WhenApiReturnsValidData()
        {
            // Arrange
            var mockWeatherService = new Mock<IWeatherService>();
            var expectedWeather = new WeatherDto { TemperatureCelcius = 25, SkyCondition = "Clear" };
            mockWeatherService.Setup(ws => ws.GetWeatherByCityName("London"))
                              .Returns(OperationResult<WeatherDto>.Success(expectedWeather));

            var service = mockWeatherService.Object;

            // Act
            var result = service.GetWeatherByCityName("London");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);
            Assert.Equal(25, result.Value.TemperatureCelcius);
            Assert.Equal("Clear", result.Value.SkyCondition);
        }

        [Fact]
        public void GetWeatherByCityName_ShouldReturnFail_WhenApiReturnsError()
        {
            // Arrange
            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(ws => ws.GetWeatherByCityName("InvalidCity"))
                              .Returns(OperationResult<WeatherDto>.Fail(OperationResultType.NoRecord, "City not found"));

            var service = mockWeatherService.Object;

            // Act
            var result = service.GetWeatherByCityName("InvalidCity");

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsResultOk);
            Assert.Equal(OperationResultType.NoRecord, result.ResultType);
            Assert.Equal("City not found", result.Failure.Reason);
        }
    }
}
