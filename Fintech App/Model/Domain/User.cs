using System;
using System.ComponentModel.DataAnnotations.Schema;
using Fintech_App.Model.Others;

namespace Fintech_App.Model.Domain
{
    [Table("Users")]
    public class User(string FirstName, string LastName, string Email, string PhoneNumber) : BaseSchema
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = FirstName;
        public string LastName { get; set; } = LastName;
        public string Email { get; set; } = Email;
        public string PhoneNumber { get; set; } = PhoneNumber;
        public ICollection<Account> Accounts { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
