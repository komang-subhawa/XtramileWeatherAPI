using Microsoft.AspNetCore.Mvc;
using Moq;
using Weather.Domain.DTOs;
using Weather.Domain.Enums;
using Weather.Service.Weather;
using WeatherAPI.Controllers;
using Xunit;

namespace Weather.UnitTest.Controller
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly WeatherController _controller;

        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _controller = new WeatherController(_mockWeatherService.Object);
        }

        [Fact]
        public void Get_ShouldReturnOk_WhenWeatherServiceReturnsSuccess()
        {
            // Arrange
            var city = "London";
            var weatherDto = new WeatherDto
            {
                SkyCondition = "Cloudy",
                TemperatureCelcius = 20,
                TemperatureFahrenheit = 68,
                RelativeHumidity = 80,
                Pressure = 1012
            };

            _mockWeatherService
                .Setup(service => service.GetWeatherByCityName(city))
                .Returns(OperationResult<WeatherDto>.Success(weatherDto));

            // Act
            var result = _controller.Get(city);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var returnedWeather = Assert.IsType<WeatherDto>(objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal("Cloudy", returnedWeather.SkyCondition);
            Assert.Equal(20, returnedWeather.TemperatureCelcius);
        }

        [Fact]
        public void Get_ShouldReturnBadRequest_WhenWeatherServiceFails()
        {
            // Arrange
            var city = "InvalidCity";
            _mockWeatherService
                .Setup(service => service.GetWeatherByCityName(city))
                .Returns(OperationResult<WeatherDto>.Fail(OperationResultType.InvalidArguments, "City not found"));

            // Act
            var result = _controller.Get(city);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var returnedFailure = Assert.IsType<OperationResult<WeatherDto>>(objectResult.Value);
            
            Assert.Equal(400, objectResult.StatusCode);            
            Assert.False(returnedFailure.IsResultOk);
            Assert.Equal(OperationResultType.InvalidArguments, returnedFailure.Failure.FailureType);
            Assert.Equal("City not found", returnedFailure.Failure.Reason);
        }
    }
}
