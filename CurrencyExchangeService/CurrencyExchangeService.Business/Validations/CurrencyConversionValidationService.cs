using CurrencyExchangeService.Business.Requests;

namespace CurrencyExchangeService.Business.Validations;

public interface ICurrencyConversionValidationService
{
    void Validate(ExchangeRequest request);
}
public class CurrencyConversionValidationService(CurrencyConversionRequestValidator currencyConversionRequestValidator) : ICurrencyConversionValidationService
{
    private readonly CurrencyConversionRequestValidator _currencyConversionRequestValidator = currencyConversionRequestValidator;

    public void Validate(ExchangeRequest request)
    {
        _currencyConversionRequestValidator.Validate(request).ProcessValidationResult();
    }
}