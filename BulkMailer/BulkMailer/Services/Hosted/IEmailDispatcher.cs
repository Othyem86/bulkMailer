namespace BulkMailer.Services.Hosted;

public interface IEmailDispatcher
{
    Task<bool> SendEmailsAsync(IEnumerable<string> addresses);
}