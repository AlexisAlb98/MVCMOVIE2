using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Diagnostics;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public ActionResult ToggleTheme()
        {
            // Get the desired theme value (light/dark) from the request
            string theme = Request.Form["theme"]; // Assuming theme is passed as a form field

            // Update the Theme variable in ViewData
            ViewData["Theme"] = theme; // Set the theme based on the value from the request

            // Redirect to the current page or desired page
            return RedirectToAction("Index"); // Redirect to the Index action of the HomeController
        }

    }
}