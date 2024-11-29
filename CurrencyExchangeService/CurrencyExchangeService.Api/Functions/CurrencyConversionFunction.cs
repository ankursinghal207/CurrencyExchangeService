using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrencyExchangeService.Api.Services;
using Microsoft.Extensions.Logging;

using Microsoft.Azure.Functions.Worker;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Validations;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
using System.Net;

namespace CurrencyExchangeService.Api.Functions;

public class CurrencyConversionFunction
{
    private readonly ILogger<CurrencyConversionFunction> _logger;
    private readonly ICurrencyConversionService _currencyConversionService;
    private readonly ICurrencyConversionValidationService _currencyConversionValidationService;
    public CurrencyConversionFunction(ILogger<CurrencyConversionFunction> logger,
        ICurrencyConversionService currencyConversionService,
        ICurrencyConversionValidationService currencyConversionValidationService)
    {
        _logger = logger;
        _currencyConversionService = currencyConversionService;
        _currencyConversionValidationService = currencyConversionValidationService;
    }

    [Function("PostCurrencyConvertAsync")]
    public async Task<IActionResult> PostCurrencyConvertAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ExchangeService")]
        [FromBody] ExchangeRequest request)
    {
        _logger.LogInformation("PostCurrencyConvertAsync function received a request.");

        try
        {
            _currencyConversionValidationService.Validate(request);

            var response = await _currencyConversionService.ConvertAudToUsdAsync(request);

            return new OkObjectResult(response);
        }
        catch (CurrencyConversionFailedException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            _logger.LogError(ex, "Invalid request");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute PostCurrencyConvertAsync function");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}