using System;

namespace Fintech_App.Model.Others
{
    public interface BaseSchema
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
