using FluentValidation.Results;
namespace CurrencyExchangeService.Business.Validations;

public static class ProcessValidationResults
{
    public static void ProcessValidationResult(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

        if (!validationResult.IsValid)
        {
            throw new CurrencyConversionFailedException(string.Join(Environment.NewLine, errors), System.Net.HttpStatusCode.Forbidden);
        }
    }
}