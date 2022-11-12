using System.ComponentModel.DataAnnotations;

namespace BulkMailer.Models
{
    public class EmailRecipient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// True, if the recipient has not yet received an email.
        /// </summary>
        [Required]
        public bool IsPending { get; set; }
    }
}
