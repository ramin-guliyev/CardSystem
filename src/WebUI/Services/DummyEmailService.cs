namespace WebUI.Services;

public class DummyEmailService : IMailService
{
    public Task SendEmailAsync(string toEmail, string subject, string content)
    {
       return Task.CompletedTask;
    }
}
