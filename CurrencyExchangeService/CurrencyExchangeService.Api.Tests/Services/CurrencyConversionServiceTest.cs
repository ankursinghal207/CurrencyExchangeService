using CurrencyExchangeService.Api.Services;
using Microsoft.Extensions.Logging;

namespace CurrencyExchangeService.Api.Tests.Services;

public class CurrencyConversionServiceTest
{

    private readonly HttpClient _httpClient = Substitute.For<HttpClient>();
    private readonly ILogger<CurrencyConversionService> _logger = Substitute.For<ILogger<CurrencyConversionService>>();

    private readonly ICurrencyConversionService _currencyConversionService;
    public CurrencyConversionServiceTest()
    {
        _currencyConversionService = new CurrencyConversionService(_logger, _httpClient);
    }

    [Fact]
    public async Task ConvertAudToUsdAsync_Success()
    {
        // Arrange
        var req = TestData.request;

        // Act
        var response = await _currencyConversionService.ConvertAudToUsdAsync(req);

        // Assert        
        response.Value.Should().BeGreaterThanOrEqualTo(0);
    }
}
