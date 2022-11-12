using BulkMailer.Models;

namespace BulkMailer.Services.Emails
{
    public interface IEmailContentProvider
    {
        EmailContent GetDefaultEmailContent();
    }
}
