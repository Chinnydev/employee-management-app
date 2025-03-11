using Microsoft.EntityFrameworkCore;
using EmployeeManagement.App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeManagement.App.Data
{
    //DbContext
    //IdentityDbContext<ApplicationUser>
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        // Your application tables:
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data if needed
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    Address = "123 Main St",
                    Position = "Developer",
                    DateOfBirth = new DateTime(1990, 1, 1)
                }
            );
        }
    }
}
