using BulkMailer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BulkMailer.Services.Emails
{
    public class SendGridDispatcher : IEmailDispatcher
    {
        private const bool SHOW_ALL_RECIPIENTS = false;

        public async Task<bool> SendMultipleEmailsAsync(
            IEnumerable<string?> addresses, 
            EmailContent emailContent)
        {
            var recipients = addresses
                .Where(r => !string.IsNullOrWhiteSpace(r))
                .Select(address => new EmailAddress(address))
                .ToList();

            if (!recipients.Any()) return false;

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);

            var sender = new EmailAddress(emailContent.From);
            var title = emailContent.Subject;
            var plainTextContent = emailContent.PlainTextContent;
            var htmlContent = string.Empty;

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                sender,
                recipients,
                title,
                plainTextContent,
                htmlContent,
                SHOW_ALL_RECIPIENTS
            );
            var response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
    }
}
