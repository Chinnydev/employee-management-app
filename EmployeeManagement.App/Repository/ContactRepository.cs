using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeManagement.App.Data;
using EmployeeManagement.App.Models;
using EmployeeManagement.App.Repository;

namespace EmployeeManagement.App.Repository 
{
    public class ContactRepository : IContactRepository
    {
        private readonly EmployeeDbContext _context;

        public ContactRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(ContactRequest contactRequest)
        {
            await _context.ContactRequests.AddAsync(contactRequest);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
