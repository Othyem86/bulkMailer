using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BulkMailer.Services.Validation
{
    public class EmailAddressValidator : IEmailAddressValidator
    {
        private readonly EmailAddressAttribute _emailAddressAttribute = new();

        public bool IsValidEmailAdress(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   _emailAddressAttribute.IsValid(email);
        }
    }
}
