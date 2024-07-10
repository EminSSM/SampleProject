using Entities;

namespace SampleProject.Mail
{
	public interface IEmailService
	{
		Task SendWelcomeEmail(string ToEmail);
        Task SendMeetingDetailsEmail(string toEmail, Meeting meeting);
    }
}
