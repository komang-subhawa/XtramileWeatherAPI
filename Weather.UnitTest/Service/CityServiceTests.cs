using Weather.Domain.Enums;
using Weather.Service.Cities;
using Xunit;

namespace Weather.UnitTest.Service
{
    public class CityServiceTests
    {
        private readonly CityService service;

        public CityServiceTests()
        {
            service = new CityService();
        }

        [Fact]
        public void GetByCountryId_ShouldReturnCities_WhenCountryExists()
        {
            // Arrange
            var validCountryId ="USA";

            // Act
            var result = service.GetByCountryId(validCountryId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);
            Assert.Contains(result.Value, c => c.Id == "NYC" && c.Name == "New York");
            Assert.Contains(result.Value, c => c.Id == "LAX" && c.Name == "Los Angeles");
            Assert.Contains(result.Value, c => c.Id == "CHI" && c.Name == "Chicago");
            Assert.DoesNotContain(result.Value, c => c.Id == "LON" || c.Name == "London");
        }

        [Fact]
        public void GetByCityId_ShouldReturnSuccess_WhenCityExists()
        {
            // Arrange
            var validCityId = "NYC";

            // Act
            var result = service.GetByCityId(validCityId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);
            Assert.Equal("New York", result.Value.Name);
            Assert.Equal("USA", result.Value.CountryId);
        }

        [Fact]
        public void GetByCityId_ShouldReturnFail_WhenCityDoesNotExist()
        {
            // Arrange
            var invalidCityId = "XYZ";

            // Act
            var result = service.GetByCityId(invalidCityId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsResultOk);
            Assert.Equal(OperationResultType.NoRecord, result.ResultType);
            Assert.Equal("City not found", result.Failure.Reason);
        }
    }
}
