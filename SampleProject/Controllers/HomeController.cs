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
        private readonly IRepository<Register> _entityRepository;
        public HomeController(ILogger<HomeController> logger, IRepository<Register> entityRepository)
        {
            _logger = logger;
            _entityRepository = entityRepository;
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
        public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Login(string email, string pass)
		{
			// Kullanýcý doðrulama iþlemi
			var user = _entityRepository.GetByData(u => u.Email == email && u.PasswordHash == pass);
			if (user != null)
			{
				return RedirectToAction("Meeting");
			}
			else
			{
				ViewBag.Error = "Geçersiz email veya þifre.";
				return View();
			}
		}

		public IActionResult Meeting()
		{
			return View();
		}
	}

}

