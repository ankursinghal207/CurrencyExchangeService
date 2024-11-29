using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;

namespace CurrencyExchangeService.Api.Tests;

public static class TestData
{
    public static readonly ExchangeRequest request = new(5, "AUD", "USD");

    public static readonly ExchangeResponse response = new(5, "AUD", "USD", 3.2);
}
