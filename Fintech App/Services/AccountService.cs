using System;
using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;
using Fintech_App.Model.ServiceInterfaces;

namespace Fintech_App.Services
{
    public class AccountService : IAccount
    {
        public Task<Account> CreateAccount(CreateAccountDTO payload)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccount()
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAccounts(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
