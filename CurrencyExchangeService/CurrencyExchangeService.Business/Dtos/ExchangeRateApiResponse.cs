
namespace CurrencyExchangeService.Business.Dtos;

public record ExchangeRateSuccessResponse(string Result, string Base_code, string Target_code, float Conversion_rate, float Conversion_result);
