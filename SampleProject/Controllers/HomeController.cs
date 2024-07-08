using Entities;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Models;
using System.Diagnostics;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(Register register)
        {
            return View();
        }

    
    }
}
