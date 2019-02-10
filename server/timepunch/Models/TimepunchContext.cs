using Microsoft.EntityFrameworkCore;

namespace timepunch.Models
{
    public class TimepunchContext : DbContext
    {
        public TimepunchContext(DbContextOptions<TimepunchContext> options) : base(options) {}
        public DbSet<IUserModel> Users { get; set; }
    }
}