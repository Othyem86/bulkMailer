using BulkMailer.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkMailer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EmailRecipient> Recipients => Set<EmailRecipient>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
