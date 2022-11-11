namespace BulkMailer.Services
{
    public interface IEmailAddressValidator
    {
        bool IsValidEmailAdress(string email);
    }
}
