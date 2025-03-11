using EmployeeManagement.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> SaveChangesAsync();
    }
}


