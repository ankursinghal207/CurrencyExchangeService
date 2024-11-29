using CurrencyExchangeService.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Logging;
using CurrencyExchangeService.Business.Validations;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddLogging(logging => logging.AddConsole());
builder.Services.AddHttpClient<ICurrencyConversionService, CurrencyConversionService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<CurrencyConversionRequestValidator>();
builder.Services.AddScoped<ICurrencyConversionValidationService, CurrencyConversionValidationService>();

builder.Build().Run();
