using BulkMailer.Models;

namespace BulkMailer.Services
{
    public interface IEmailRecipientsService
    {
        Task CreateRecipientAsync(EmailRecipient recipient);

        // TODO: this is only for testing purposes
        Task<IEnumerable<EmailRecipient>> GetAllRecipientsAsync();
    }
}
