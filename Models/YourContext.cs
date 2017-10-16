using Microsoft.EntityFrameworkCore;
 
namespace bank_project.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
    }
}