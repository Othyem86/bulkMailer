using BulkMailer.Data;
using BulkMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkMailer.Services.Emails
{
    public class EmailRecipientService : IEmailRecipientsService
    {
        private readonly AppDbContext _appDbContext;

        public EmailRecipientService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateRecipientAsync(EmailRecipient recipient)
        {
            await _appDbContext.Recipients.AddAsync(recipient);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<EmailRecipient?> GetRecipientAsync(int id)
        {
            return await _appDbContext.Recipients.FindAsync(id);
        }

        public async Task<bool> UpdateRecipientAsync(int id, EmailRecipient recipient)
        {
            var foundRecipient = await _appDbContext.Recipients.FindAsync(id);

            if (foundRecipient is null) return false;

            foundRecipient.Email = recipient.Email;
            foundRecipient.IsPending = recipient.IsPending;

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmailRecipient>> GetAllPendingRecipientsAsync()
        {
            var recipients = await _appDbContext.Recipients
                .Where(r => r.IsPending == true)
                .ToListAsync();

            return recipients;
        }

        public async Task<IEnumerable<EmailRecipient>> GetAllRecipientsAsync()
        {
            var recipients = await _appDbContext.Recipients.ToListAsync();

            return recipients;
        }
    }
}
