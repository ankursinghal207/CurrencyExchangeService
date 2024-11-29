using System.Net;

namespace CurrencyExchangeService.Business.Validations;

public class CurrencyConversionFailedException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}
