using Microsoft.EntityFrameworkCore;

namespace timepunch.Models
{
    public class TimepunchContext : DbContext
    {
        public TimepunchContext(DbContextOptions<TimepunchContext> options) : base(options) {}
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserProfileModel> UserProfiles { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<ShiftModel> Shifts { get; set; }
    }
}