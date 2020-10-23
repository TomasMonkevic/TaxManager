using Microsoft.EntityFrameworkCore;
using TaxManager.Domain;

namespace TaxManager.Persistence
{
    public class TaxManagerContext : DbContext
    {
        public DbSet<Municipality> Manucipalities { get; set; }

        public TaxManagerContext(DbContextOptions<TaxManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().HasKey(m => m.Name);
            modelBuilder.Entity<Municipality>().OwnsMany<Tax>(m => m.Taxes);
        }

    }
}
