using System.Diagnostics;
using System.Threading.Tasks;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;
using EmployeeManagement.App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactService _contactService;

        public HomeController(ILogger<HomeController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactCreateDto contactCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(contactCreateDto);
            }

            var result = await _contactService.CreateContactAsync(contactCreateDto);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Contact");
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
                return View(contactCreateDto);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




//using System.Diagnostics;
//using EmployeeManagement.App.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace EmployeeManagement.App.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        public IActionResult About()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Contact()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Contact(string Name, string Email, string Message)
//        {
//            // Handle form submission (e.g., save to database, send email)
//            ViewData["Message"] = "Thank you for contacting us. We will get back to you soon!";
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
