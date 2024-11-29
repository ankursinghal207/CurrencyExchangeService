using CurrencyExchangeService.Api.Functions;
using CurrencyExchangeService.Api.Services;
using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyExchangeService.Api.Tests.Functions;

public class CurrencyConversionFunctionTest
{
    private readonly CurrencyConversionFunction _currencyConversionFunction;
    private readonly ICurrencyConversionService _currencyConversionService = Substitute.For<ICurrencyConversionService>();
    private readonly ICurrencyConversionValidationService _currencyConversionValidationService = Substitute.For<ICurrencyConversionValidationService>();
    private readonly ILogger<CurrencyConversionFunction> _logger = Substitute.For<ILogger<CurrencyConversionFunction>>();
    public CurrencyConversionFunctionTest()
    {
        _currencyConversionFunction = new CurrencyConversionFunction(_logger, _currencyConversionService, _currencyConversionValidationService);
    }

    [Fact]
    public async Task PostCurrencyConvertAsync_Success()
    {
        // Arrange
        _currencyConversionService.ConvertAudToUsdAsync(Arg.Any<ExchangeRequest>()).Returns(TestData.response);

        // Act
        var response = await _currencyConversionFunction.PostCurrencyConvertAsync(TestData.request);

        // Assert
        response.Should().BeEquivalentTo(new OkObjectResult(TestData.response));
    }

    [Fact]
    public async Task PostCurrencyConvertAsync_Error()
    {
        // Arrange
        var exception = new Exception("err");
        _currencyConversionService.When(x => x.ConvertAudToUsdAsync(Arg.Any<ExchangeRequest>())).Do(x => throw exception);

        // Act
        var response = await _currencyConversionFunction.PostCurrencyConvertAsync(TestData.request);

        // Assert
        response.Should().BeEquivalentTo(new StatusCodeResult(StatusCodes.Status500InternalServerError));
    }
}