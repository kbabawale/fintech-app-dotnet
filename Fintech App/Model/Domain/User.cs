using System;
using System.ComponentModel.DataAnnotations.Schema;
using Fintech_App.Model.Others;

namespace Fintech_App.Model.Domain
{
    [Table("Users")]
    public class User : BaseSchema
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public ICollection<Account> Accounts { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
