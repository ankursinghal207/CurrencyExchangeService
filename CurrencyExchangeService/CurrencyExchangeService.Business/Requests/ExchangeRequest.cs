namespace CurrencyExchangeService.Business.Requests;

public record ExchangeRequest(double Amount, string InputCurrency, string OutputCurrency);
