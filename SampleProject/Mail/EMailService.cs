using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;


namespace SampleProject.Mail
{
	public class EmailSettings
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public bool EnableSsl { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class EMailService : IEmailService
	{
		private readonly IConfiguration _configuration;
		private readonly EmailSettings _emailSettings;

		public EMailService(IConfiguration configuration, IOptions<EmailSettings> emailSettings)
		{
			_configuration = configuration;
			_emailSettings = emailSettings.Value;
		}

		public async Task SendWelcomeEmail(string ToEmail)
		{
			var smptClient = new System.Net.Mail.SmtpClient();

			smptClient.Host = _emailSettings.Host;
			smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smptClient.UseDefaultCredentials = false;
			smptClient.Port = 587;
			smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
			smptClient.EnableSsl = true;

			var mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(_emailSettings.Email, "DemoProje");
			mailMessage.To.Add(ToEmail);

			mailMessage.Subject = "Hoş Geldiniz";
			mailMessage.Body = @$"
            <h3>Merhaba,</h3>
            <p>hoş geldiniz!</p>
            <p>Sizlerle birlikte daha güzel işler başaracağımıza inanıyoruz.</p>
            <p>Teşekkür ederiz.</p>";

			mailMessage.IsBodyHtml = true;

			await smptClient.SendMailAsync(mailMessage);
		}
	}
}

