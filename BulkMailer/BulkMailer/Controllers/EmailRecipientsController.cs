using BulkMailer.Contracts;
using BulkMailer.Models;
using BulkMailer.Services.Emails;
using BulkMailer.Services.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BulkMailer.Controllers;

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
        if (string.IsNullOrWhiteSpace(request.Emails))
            return BadRequest("You entered a wrong formatted email. Please try again.");

        var separators = new [] { ' ', ',', ';' };
        var addresses = request.Emails.Split(
            separators,
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var validAddresses = new List<EmailRecipientResponse>();

        foreach (var address in addresses)
        {
            if (!_eMailValidator.IsValidEmailAdress(address))
                return BadRequest("You entered a wrong formatted email. Please try again.");
                
            // Map requests to recipients
            var recipient = new EmailRecipient
            {
                Email = address,
                IsPending = true
            };

            await _eMailRecipientsService.CreateRecipientAsync(recipient);

            validAddresses.Add(new EmailRecipientResponse(
                recipient.Email, 
                recipient.IsPending));
        }
        // TODO: what's the proper response etiquette in a scenario with no get entpoint?
        // return Ok(validAddresses);

        return CreatedAtAction(
            nameof(GetAllEmailRecipients), 
            validAddresses);
    }

    // TODO: This endpoint is for test purposes only
    [HttpGet]
    public async Task<IActionResult> GetAllEmailRecipients()
    {
        var emailRecipients = await _eMailRecipientsService.GetAllRecipientsAsync();

        // Map recipients to responses
        var emailRecipientResponses = emailRecipients
            .Select(r => new EmailRecipientResponse(r.Email, r.IsPending))
            .ToList();

        return Ok(emailRecipientResponses);
    }
}