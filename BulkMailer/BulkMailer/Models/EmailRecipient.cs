using System.ComponentModel.DataAnnotations;

namespace BulkMailer.Models
{
    public class EmailRecipient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool ReceivedEmail { get; set; }
    }
}
