namespace BulkMailer.Services.Validation
{
    public interface IEmailAddressValidator
    {
        bool IsValidEmailAdress(string email);
    }
}
