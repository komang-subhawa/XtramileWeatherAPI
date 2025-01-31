using Weather.Domain.Enums;
using Weather.Service.Countries;
using Xunit;

namespace Weather.UnitTest.Service
{
    public class CountryServiceTests
    {
        private readonly CountryService service;

        public CountryServiceTests()
        {
            service = new CountryService();
        }

        [Fact]
        public void GetAll_ShouldReturnSuccessWithMockData()
        {
            // Act
            var result = service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);

            Assert.Contains(result.Value, c => c.Id == "USA" && c.Name == "United States");
            Assert.Contains(result.Value, c => c.Id == "GBR" && c.Name == "United Kingdom");
            Assert.Contains(result.Value, c => c.Id == "FRA" && c.Name == "France");
            Assert.Contains(result.Value, c => c.Id == "JPN" && c.Name == "Japan");
            Assert.Contains(result.Value, c => c.Id == "DEU" && c.Name == "Germany");
            Assert.DoesNotContain(result.Value, c => c.Name == "Bali");
        }

        [Fact]
        public void GetByCountryId_ShouldReturnSuccess_WhenCountryExists()
        {
            // Arrange
            var validCountryId = "USA";

            // Act
            var result = service.GetByCountryId(validCountryId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsResultOk);
            Assert.NotNull(result.Value);
            Assert.Equal("United States", result.Value.Name);
        }

        [Fact]
        public void GetByCountryId_ShouldReturnFail_WhenCountryDoesNotExist()
        {
            // Arrange
            var invalidCountryId = "XYZ";

            // Act
            var result = service.GetByCountryId(invalidCountryId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsResultOk);
            Assert.Equal(OperationResultType.NoRecord, result.ResultType);
            Assert.Equal("Country not found", result.Failure.Reason);
        }
    }
}
