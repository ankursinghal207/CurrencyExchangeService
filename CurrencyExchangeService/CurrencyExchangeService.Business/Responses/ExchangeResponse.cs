namespace CurrencyExchangeService.Business.Responses;

public record ExchangeResponse(double Amount, string InputCurrency, string OutputCurrency, double Value);
