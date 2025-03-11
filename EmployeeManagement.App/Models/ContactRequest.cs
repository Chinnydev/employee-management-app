using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.App.Models
{
    public class ContactRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}

