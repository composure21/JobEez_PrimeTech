using Microsoft.AspNetCore.Identity.UI.Services;

namespace JobEez_App.Models
{
    public class DummyEmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing
            return Task.CompletedTask;
        }
    }
}
