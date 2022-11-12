namespace BulkMailer.Models;

public class EmailContent
{
    public string? From { get; set; }

    public string? Subject { get; set; }

    public string? PlainTextContent { get; set; }
}