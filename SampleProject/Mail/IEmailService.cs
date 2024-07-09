namespace SampleProject.Mail
{
	public interface IEmailService
	{
		Task SendWelcomeEmail(string ToEmail);
	}
}
