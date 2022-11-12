using BulkMailer.Models;

namespace BulkMailer.Services.Emails
{
    public interface IEmailRecipientsService
    {
        Task CreateRecipientAsync(EmailRecipient recipient);

        Task<EmailRecipient?> GetRecipientAsync(int id);

        Task<bool> UpdateRecipientAsync(int id, EmailRecipient recipient);

        Task<IEnumerable<EmailRecipient>> GetAllPendingRecipientsAsync();

        Task<IEnumerable<EmailRecipient>> GetAllRecipientsAsync();
    }
}
