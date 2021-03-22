using Microsoft.EntityFrameworkCore;

namespace Aptos.Data
{
    public class AptosDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuilder)
        {
            dbContextOptionBuilder.UseSqlite("Data Source=AptosDB.db");
        }
    }
}