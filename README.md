# CurrencyExchangeService

This repository holds the currency conversion service code. The objective of this service is to convert the currency from AUD to USD only for a provided amount.

## How to run

- Clone the repository
- Open the `CurrencyExchangeService.sln` file in Visual Studio
- Press the Play button (or press F5 on windows machines)
- The code will run and expose the endpoint `http://localhost:7287/ExchangeService` for post requests
- Run the curl command from cmd terminal
```
    curl -X 'POST' \
   'http://localhost:7287/ExchangeService' \
   -H 'accept: text/plain' \
   -H 'Content-Type: application/json' \
   -d '{
   "amount": 5,
   "inputCurrency": "AUD",
   "outputCurrency": "USD"
    }'
```

## Further potential enhancements for production
- Support for additional currencies
- I would implement caching to cache the response from ExchangeRate-Api as it does not change frequently.
- Secure the Http end point
- Add more validations
- More test coverage
- Given task is to create the `POST` request, but code makes `GET` request to ExchangeRate-Api. I suggest to change the client API call to `GET` request instead of `POST`
