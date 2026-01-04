using System;
using System.Text.Json;
using Fintech_App.Exceptions;
using Fintech_App.Model.Db;
using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;
using Fintech_App.Model.Response;
using Fintech_App.Model.ServiceInterfaces;
using Fintech_App.Util;
using Microsoft.EntityFrameworkCore;

namespace Fintech_App.Services
{
    public class AccountService(ILogger<AccountService> logger, AppDbContext appDbContext) : IAccount
    {

        private readonly ILogger<AccountService> _logger = logger;

        public async Task<bool> ChangePin(ChangePinDTO payload)
        {
            try
            {
                var sender = await appDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == payload.AccountNumber) ?? throw new ValidationException("Account Number is invalid");

                var _pinService = new PinService();
                if (!_pinService.VerifyPin(payload.OldPin.ToString(), sender.Pin))
                {
                    throw new ValidationException("Provide correct current Pin");
                }

                {
                    var newPin = _pinService.HashPin(payload.NewPin.ToString());
                    sender.Pin = newPin;
                    await appDbContext.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountDTO payload)
        {
            try
            {
                //generate accountNumber
                var accountNumber = AccountNumberGenerator.GenerateAccountNumber();

                //hash pin
                var hashedPin = new PinService().HashPin(payload.Pin.ToString());

                //create User in DB
                var userPayload = new User(payload.FirstName.Trim(), payload.LastName.Trim(), payload.Email.Trim(), "");
                var user = appDbContext.Users.Add(userPayload);


                //create Account in DB
                var account = new Account((int)accountNumber, user.Entity.Id, hashedPin, Model.Currency.NGN);
                appDbContext.Accounts.Add(account);

                //DB operation is atomic. If create account fails, created user will be rolled back
                await appDbContext.SaveChangesAsync();

                return new CreateAccountResponse(
                    account.Id,
                    account.AccountNumber,
                    account.Balance,
                    account.Currency
                );

            }
            catch (Exception)
            {
                throw;
            }


        }

        public Task<Account> GetAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAccountResponse>> GetAccounts(string Email)
        {
            try
            {
                //Find accounts using user email 
                var accounts = await appDbContext.Accounts
                .Where(x => x.User.Email == Email.Trim())
                .Select(p => new GetAccountResponse(
                    p.AccountNumber,
                    p.Balance,
                    p.Currency,
                    $"{p.User.FirstName} {p.User.LastName}"
                ))
                .ToListAsync();

                // _logger.LogInformation(JsonSerializer.Serialize(accounts));

                return accounts;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> MakeTransfer(MakeTransferDTO payload)
        {
            try
            {
                //find both accounts
                //ensure sender has enough money
                //execute transfer atomically with idempotency keys
                var sender = await appDbContext.Accounts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.AccountNumber == payload.SenderAccountNumber) ?? throw new ValidationException("Sender Account Number is not valid");

                if (new PinService().VerifyPin(payload.SenderPin.ToString(), sender.Pin) == false)
                {
                    throw new ValidationException("Sender Pin is wrong");
                }

                var recipient = await appDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == payload.ReceipientAccountNumber) ?? throw new ValidationException("Recipient Account Number is not valid");

                if (sender.Balance < payload.Amount) throw new ValidationException($"Sender ({sender.User.FirstName}, {sender.User.LastName}) does not have sufficient balance");

                {
                    sender.Balance -= payload.Amount;
                    recipient.Balance += payload.Amount;
                    await appDbContext.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
