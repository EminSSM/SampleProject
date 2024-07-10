using DataContext.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Mail;
using System.Security.Cryptography;
using System.Text;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Register> _entityRepository;
        private readonly IRepository<Meeting> _meetingRepository;
		private readonly IEmailService _emailService;
        public HomeController(IRepository<Register> entityRepository, IEmailService emailService, IRepository<Meeting> meetingRepository)
        {
            _entityRepository = entityRepository;
            _emailService = emailService;
            _meetingRepository = meetingRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                // Þifreyi hashle
                register.PasswordHash = HashPassword(register.PasswordHash);

                _entityRepository.Add(register);

				_emailService.SendWelcomeEmail(register.Email);
				return RedirectToAction("Login");
            }

            return View(register);

        }
        public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Login(string email, string pass)
		{
            var hashedPassword = HashPassword(pass);
            var user = _entityRepository.GetByData(u => u.Email == email && u.PasswordHash == hashedPassword);
            if (user != null)
			{
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Meeting");
			}
			else
			{
				ViewBag.Error = "Geçersiz email veya þifre.";
				return View();
			}
		}
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Þifreyi hashledim
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public IActionResult Meeting()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Meeting(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _meetingRepository.Add(meeting);
                var userEmail = HttpContext.Session.GetString("UserEmail");

                if (!string.IsNullOrEmpty(userEmail))
                {
                   _emailService.SendMeetingDetailsEmail(userEmail, meeting);
                }

                return RedirectToAction("Meeting");
            }

            return View(meeting);
        }
    }

}

