using BulkMailer.Data;
using System.Runtime;
using BulkMailer.Models;
using BulkMailer.Services.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace BulkMailer.Services.Background
{
    public class EmailDispatcherService : ScopedProcessor
    {
        public EmailDispatcherService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var emailService = serviceProvider
                .GetService(typeof(IEmailRecipientsService)) as IEmailRecipientsService;

            if (emailService is null)
                return;

            var recipient = new EmailRecipient
            {
                Email = "Test",
                IsPending = true
            };

            await emailService.CreateRecipientAsync(recipient);
        }
    }
}
