using Api.Security;
using Api.ViewModel;
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
		//private readonly IRepository<Register> _entityRepository;

		public LoginController(IConfiguration configuration)
		{
			_configuration = configuration;
			//_entityRepository = entityRepository;
		}

		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate([FromBody] LoginModel model)
		{
				var token = TokenHandler.CreateToken(_configuration, model.Email);
				return Ok(new { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken, Expiration = token.Expiration });

		}

		private string HashPassword(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
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

public class LoginModel
{
	public string Email { get; set; }
	public string Password { get; set; }
}