using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.App.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
