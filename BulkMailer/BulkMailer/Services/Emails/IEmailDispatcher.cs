using BulkMailer.Models;

namespace BulkMailer.Services.Emails;

public interface IEmailDispatcher
{
    Task<bool> SendMultipleEmailsAsync(IEnumerable<string?> addresses, EmailContent emailContent);
}