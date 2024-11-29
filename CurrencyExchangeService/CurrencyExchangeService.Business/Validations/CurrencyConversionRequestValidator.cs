using CurrencyExchangeService.Business.Requests;
using FluentValidation;

namespace CurrencyExchangeService.Business.Validations;
public class CurrencyConversionRequestValidator : AbstractValidator<ExchangeRequest>
{
    public CurrencyConversionRequestValidator()
    {
        RuleFor(x => x).NotEmpty();

        RuleFor(x => x.Amount)
        .NotEmpty()
        .GreaterThanOrEqualTo(0);

        RuleFor(x => x.InputCurrency)
        .Equal("AUD", StringComparer.OrdinalIgnoreCase)
        .WithMessage("Invalid input currency, please use only AUD")
        .NotEqual(x => x.OutputCurrency, StringComparer.OrdinalIgnoreCase)
        .WithMessage("Input currency and output currency type cannot be same");

        RuleFor(x => x.OutputCurrency)
        .Equal("USD", StringComparer.OrdinalIgnoreCase)
        .WithMessage("Invalid output currency, please use only USD");
    }
}

