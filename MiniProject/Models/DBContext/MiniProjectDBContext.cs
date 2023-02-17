using Microsoft.EntityFrameworkCore;

namespace MiniProject.Models.DBContext
{
    public class MiniProjectDBContext : DbContext
    {
        public MiniProjectDBContext(DbContextOptions<MiniProjectDBContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employees> Employees { get; set; }
    }
}
