using Microsoft.AspNetCore.Mvc;

namespace Fintech_App.Model.DTO
{
    //Record was added in C# 9 and later, so Class was used instead. 
    //See more info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record
    public sealed record GetAccountsDTO(
        [FromRoute] string Email
    );
}
