using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeCreateDto);
                // Returns a 201 Created response with a Location header pointing to the new resource.
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(id, employeeUpdateDto);
                if (!result)
                {
                    return NotFound();
                }
                // Returns 204 No Content on a successful update
                return NoContent();
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                // Returns 204 No Content on a successful deletion
                return NoContent();
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception
                return StatusCode(500, ex.Message);
            }
        }
    }
}
