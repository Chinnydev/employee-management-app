using EmployeeManagement.App.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAllEmployeesAsync();
        Task<EmployeeReadDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeReadDto> CreateEmployeeAsync(EmployeeCreateDto employeeCreateDto);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}




