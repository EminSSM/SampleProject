using DataContext.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Models;
using System.Diagnostics;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<BaseEntity> _entityRepository;
        public HomeController(ILogger<HomeController> logger, IRepository<BaseEntity> entityRepository)
        {
            _logger = logger;
            _entityRepository = entityRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (register != null)
            {
                _entityRepository.Add(register);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

        }


    }
}
