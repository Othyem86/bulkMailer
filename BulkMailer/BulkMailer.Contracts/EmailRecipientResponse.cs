namespace BulkMailer.Contracts;

public record EmailRecipientResponse(string? Email, bool IsPending);