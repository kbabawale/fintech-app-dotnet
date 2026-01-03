using System;
using System.ComponentModel.DataAnnotations.Schema;
using Fintech_App.Model.Others;

namespace Fintech_App.Model.Domain
{
    [Table("Accounts")]
    public class Account : BaseSchema
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; } = 0;
        public Currency Currency { get; set; } = Currency.NGN;
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
