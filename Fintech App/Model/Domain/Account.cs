using System;
using System.ComponentModel.DataAnnotations.Schema;
using Fintech_App.Model.Others;

namespace Fintech_App.Model.Domain
{
    [Table("Accounts")]
    public class Account(int AccountNumber, Guid userId, string Pin, Currency currency = Currency.NGN) : BaseSchema
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int AccountNumber { get; set; } = AccountNumber;
        public string Pin { get; set; } = Pin;
        public decimal Balance { get; set; } = 0;
        public Currency Currency { get; set; } = currency;
        public Guid UserId { get; set; } = userId;
        public User User { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
