using BulkMailer.Data;
using BulkMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkMailer.Services
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

        // TODO: for test purposes only
        public async Task<IEnumerable<EmailRecipient>> GetAllRecipientsAsync()
        {
            var recipients = await _appDbContext.Recipients.ToListAsync();

            return recipients;
        }
    }
}
