using Api.Security;
using Api.ViewModel;
using DataContext.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IRepository<Register> _entityRepository;

		public LoginController(IConfiguration configuration, IRepository<Register> entityRepository)
		{
			_configuration = configuration;
			_entityRepository = entityRepository;
		}

		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate([FromBody] LoginModel model)
		{
			var user = AuthenticateUser(model.Email, model.Password);

			if (user != null)
			{
				var token = TokenHandler.CreateToken(_configuration);
				return Ok(new { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken, Expiration = token.Expiration });
			}

			return Unauthorized(new { message = "Invalid email or password." });
		}

		private Register AuthenticateUser(string email, string password)
		{
			var hashedPassword = HashPassword(password);
			var user = _entityRepository.GetByData(u => u.Email == email && u.PasswordHash == hashedPassword);
			return user;
		}

		private string HashPassword(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// Compute hash.
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

				// Convert byte array to a string.
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
