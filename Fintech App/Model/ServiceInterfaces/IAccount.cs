using System;
using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;
using Fintech_App.Model.Response;

namespace Fintech_App.Model.ServiceInterfaces
{
    public interface IAccount
    {
        Task<List<GetAccountResponse>> GetAccounts(string Email);
        Task<bool> ChangePin(ChangePinDTO payload);
        Task<bool> MakeTransfer(MakeTransferDTO payload);
        Task<CreateAccountResponse> CreateAccount(CreateAccountDTO payload);
    }
}
