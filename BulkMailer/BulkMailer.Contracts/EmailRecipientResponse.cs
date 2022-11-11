namespace BulkMailer.Contracts;

public record EmailRecipientResponse(string email, bool receivedMail);