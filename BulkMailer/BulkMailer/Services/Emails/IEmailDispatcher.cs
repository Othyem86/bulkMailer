namespace BulkMailer.Services.Emails;

public interface IEmailDispatcher
{
    Task<bool> SendEmailsAsync(IEnumerable<string> addresses);
}