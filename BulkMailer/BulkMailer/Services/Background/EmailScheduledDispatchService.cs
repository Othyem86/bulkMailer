using BulkMailer.Data;
using System.Runtime;
using BulkMailer.Models;
using BulkMailer.Services.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace BulkMailer.Services.Background
{
    public class EmailScheduledDispatchService : ScopedProcessor
    {
        private const int DELAY = 10_000;

        public EmailScheduledDispatchService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            Delay = DELAY;
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            // Service locator pattern is not ideal, but necessary due to the scope
            // constraints imposed by this instance of IHostedService (Singleton) which
            // depends on the dbContext (scoped).
            if (serviceProvider.GetService(typeof(IEmailRecipientsService)) 
                    is not IEmailRecipientsService emailRecipientsService ||
                serviceProvider.GetService(typeof(IEmailDispatcher)) 
                    is not IEmailDispatcher dispatcher ||
                serviceProvider.GetService(typeof(IEmailContentProvider)) 
                    is not IEmailContentProvider contentProvider)
            {
                return;
            }

            var recipients = (await emailRecipientsService.GetAllPendingRecipientsAsync()).ToList();

            if (!recipients.Any()) return;

            var addresses = recipients.Select(r => r.Email).ToList();
            var emailContent = contentProvider.GetDefaultEmailContent();
            var mailsSent = await dispatcher.SendMultipleEmailsAsync(addresses, emailContent);

            if (!mailsSent) return;

            foreach (var recipient in recipients)
            {
                await emailRecipientsService.UpdateRecipientAsync(
                    recipient.Id,
                    new EmailRecipient
                    {
                        Id = recipient.Id,
                        Email = recipient.Email, 
                        IsPending = false
                    });
            }
        }
    }
}
