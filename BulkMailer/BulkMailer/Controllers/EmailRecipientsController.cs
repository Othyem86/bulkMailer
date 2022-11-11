using BulkMailer.Contracts;
using BulkMailer.Models;
using BulkMailer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulkMailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailRecipientsController : ControllerBase
    {
        private readonly IEmailRecipientsService _eMailRecipientsService;

        private readonly IEmailAddressValidator _eMailValidator;


        public EmailRecipientsController(
            IEmailRecipientsService recipientsService,
            IEmailAddressValidator eMailValidator)
        {
            _eMailRecipientsService = recipientsService;
            _eMailValidator = eMailValidator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmailRecipients(CreateEmailRecipientsRequest request)
        {
            var separators = new [] { ' ', ',', ';' };

            var addresses = request.emails.Split(
                separators,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var validAddresses = new List<EmailRecipientResponse>();

            foreach (var address in addresses)
            {
                if (!_eMailValidator.IsValidEmailAdress(address))
                    return BadRequest("You entered a wrong formatted email. Please try again.");
                
                var recipient = new EmailRecipient
                {
                    Email = address,
                    ReceivedEmail = false
                };

                await _eMailRecipientsService.CreateRecipientAsync(recipient);

                validAddresses.Add(new EmailRecipientResponse(address, false));
            }

            // TODO: what's the proper response etiquette in this scenario?
            // return Ok(validAddresses);

            return CreatedAtAction(
                nameof(GetAllEmailRecipients), 
                validAddresses);
        }

        // TODO: for test purposes only
        [HttpGet]
        public async Task<IActionResult> GetAllEmailRecipients()
        {
            var emailRecipients = await _eMailRecipientsService.GetAllRecipientsAsync();

            return Ok(emailRecipients);
        }
    }
}
