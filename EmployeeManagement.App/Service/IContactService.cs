using System.Threading.Tasks;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;

namespace EmployeeManagement.App.Service
{
    public interface IContactService
    {
        Task<ContactResult> CreateContactAsync(ContactCreateDto contactDto);
    }
}

