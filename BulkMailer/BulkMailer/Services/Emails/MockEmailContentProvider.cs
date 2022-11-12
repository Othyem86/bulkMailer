using BulkMailer.Models;

namespace BulkMailer.Services.Emails
{
    public class MockEmailContentProvider : IEmailContentProvider
    {
        // TODO: Replace with a dynamic resource
        private const string FROM = Environment.GetEnvironmentVariable("SENDER_EMAIL_ADDRESS");

        private const string SUBJECT = "Email Sending Assessment";

        private const string PLAIN_TEXT_CONTENT = 
            "Dear user, you have just received an email for your " +
            "as a part of the Email Sending Assessment development.";

        public EmailContent GetDefaultEmailContent()
        {
            return new EmailContent
            {
                From = FROM,
                Subject = SUBJECT,
                PlainTextContent = PLAIN_TEXT_CONTENT
            };
        }
    }
}
