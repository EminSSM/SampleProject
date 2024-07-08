using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class ContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Initial Catalog = DemoProjectDb; Integrated Security = True; Encrypt = False; Connection Timeout = 60");
        }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
