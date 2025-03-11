using AutoMapper;
using EmployeeManagement.App.Repository;
using EmployeeManagement.App.Service;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeReadDto>>(employees);
        }

        public async Task<EmployeeReadDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return null;
            }
            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<EmployeeReadDto> CreateEmployeeAsync(EmployeeCreateDto employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employeeFromRepo = await _employeeRepository.GetByIdAsync(id);
            if (employeeFromRepo == null)
            {
                return false;
            }

            _mapper.Map(employeeUpdateDto, employeeFromRepo);

            try
            {
                await _employeeRepository.UpdateAsync(employeeFromRepo);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                throw new Exception("An error occurred while updating the employee.", ex);
            }
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return false;
            }

            try
            {
                await _employeeRepository.DeleteAsync(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                throw new Exception("An error occurred while deleting the employee.", ex);
            }
            return true;
        }
    }
}


