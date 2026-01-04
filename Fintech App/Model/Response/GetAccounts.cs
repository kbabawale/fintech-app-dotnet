using Fintech_App.Model.Domain;

namespace Fintech_App.Model.Response
{
    //Record was added in C# 9 and later, so Class was used instead. 
    //See more info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record
    public record GetAccountResponse(int AccountNumber, decimal Balance, Currency Currency, string Name)
    {
        public string _currency => Currency switch
        {
            Currency.NGN => "NGN",
            Currency.USD => "USD",
            _ => "NGN"
        };
    }
}
