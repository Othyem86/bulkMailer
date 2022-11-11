namespace BulkMailer.Contracts;

/// <summary>
/// Request contract for requests to create Email adresses of one or more recipients.
/// </summary>
/// <param name="emails">
/// String representing enumerated email adresses. Comma and/or semicolon separated.
/// </param>
public record CreateEmailRecipientsRequest(string emails);