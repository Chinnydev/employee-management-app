using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.App.Controllers
{
    public class ErrorController : Controller
    {
        // This action handles status code errors (like 404)
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["ErrorMessage"] = "Sorry, the page you requested could not be found.";
                    return View("404");
                default:
                    ViewData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                    return View("Error");
            }
        }
    }
}



