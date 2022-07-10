using Forex.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forex.Data
{
    public class ForexDbContext : DbContext
    {
        public string connectionString = "host=localhost;port=5432;database=forex;username=postgres;password=postgres";

        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<CandleStick> PriceData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandleStick>()
                .HasKey(c => new { c.SymbolId, c.CloseTime, c.Interval});

            base.OnModelCreating(modelBuilder);
        }
    }
}