
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDto employeeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with the model if the model state is invalid
                return View(employeeCreateDto);
            }

            try
            {
                await _employeeService.CreateEmployeeAsync(employeeCreateDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Log exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while creating the employee.");
                return View(employeeCreateDto);
            }
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Map EmployeeReadDto to EmployeeUpdateDto manually
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                Position = employee.Position,
                DateOfBirth = employee.DateOfBirth
            };

            ViewBag.EmployeeId = employee.Id;
            return View(employeeUpdateDto);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EmployeeId = id;
                return View(employeeUpdateDto);
            }

            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(id, employeeUpdateDto);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Log exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while updating the employee.");
                ViewBag.EmployeeId = id;
                return View(employeeUpdateDto);
            }
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Log exception if needed
                return RedirectToAction(nameof(Index));
            }
        }

    }
}