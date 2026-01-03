using System;
using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;

namespace Fintech_App.Model.ServiceInterfaces
{
    public interface IAccount
    {
        Task<List<Account>> GetAccounts(Guid id);
        Task<Account> GetAccount();
        Task<Account> CreateAccount(CreateAccountDTO payload);
    }
}
