using System.Threading.Tasks;
using EmployeeManagement.App.Models;

namespace EmployeeManagement.App.Repository 
{
    public interface IContactRepository
    {
        Task AddContactAsync(ContactRequest contactRequest);
        Task SaveChangesAsync();
    }
}

