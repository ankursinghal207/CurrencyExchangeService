using CurrencyExchangeService.Business.Dtos;
using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using CurrencyExchangeService.Business.Validations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CurrencyExchangeService.Api.Services;

public interface ICurrencyConversionService
{
    public Task<ExchangeResponse> ConvertAudToUsdAsync(ExchangeRequest request);
}
public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly ILogger<CurrencyConversionService> _logger;
    private readonly HttpClient _httpClient;

    private const string _apiKey = "69d3a68c65a697cb29833952";
    private const string _exchangeServiceBaseUrl = "https://v6.exchangerate-api.com/v6/{0}/pair/{1}/{2}/{3}";

    public CurrencyConversionService(ILogger<CurrencyConversionService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<ExchangeResponse> ConvertAudToUsdAsync(ExchangeRequest request)
    {
        _logger.LogInformation("ConvertAudToUsd is called");

        var exchangeApiUri = new Uri(string.Format(_exchangeServiceBaseUrl, _apiKey, request.InputCurrency, request.OutputCurrency, request.Amount));

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, exchangeApiUri);
        var response = await _httpClient.SendAsync(httpRequest);

        var responseDto = await ProcessHttpResponse<ExchangeRateSuccessResponse>(response);

        return new ExchangeResponse(request.Amount, responseDto.Base_code, responseDto.Target_code, Math.Round(responseDto.Conversion_result, 2));
    }

    private static async Task<T> ProcessHttpResponse<T>(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        throw new CurrencyConversionFailedException(responseString, response.StatusCode);
    }
}
