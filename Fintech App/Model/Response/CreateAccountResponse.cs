using System;
using Fintech_App.Model.Domain;

namespace Fintech_App.Model.Response
{
    public record CreateAccountResponse(Guid Id, int AccountNumber, decimal Balance, Currency currency)
    {
        public string _currency => currency switch
        {
            Currency.NGN => "NGN",
            Currency.USD => "USD",
            _ => "NGN"
        };
    }
}
