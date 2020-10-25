using Microsoft.EntityFrameworkCore;
using TaxManager.Domain;

namespace TaxManager.Persistence
{
    public class TaxManagerContext : DbContext
    {
        public DbSet<Municipality> Manucipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        public TaxManagerContext(DbContextOptions<TaxManagerContext> options) 
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().HasMany(m => m.Taxes)
                                                .WithOne(t => t.Municipality)
                                                .HasForeignKey(t => t.MunicipalityId);

            modelBuilder.Entity<Municipality>().HasIndex(m => m.Name).IsUnique();
        }
    }
}
