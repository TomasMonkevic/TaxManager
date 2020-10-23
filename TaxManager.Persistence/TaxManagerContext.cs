using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaxManager.Domain;

namespace TaxManager.Persistence
{
    public class TaxManagerContext : DbContext
    {
        public DbSet<Municipality> Manucipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        public TaxManagerContext(DbContextOptions<TaxManagerContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().HasKey(m => m.Id);
            modelBuilder.Entity<Tax>().HasKey(t => t.Id);

            //modelBuilder.Entity<Municipality>().Property(m => m.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Tax>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Municipality>().HasData(
                new Municipality {
                    Id = 1, 
                    Name = "Copenhagen"

                },
                new Municipality {
                    Id = 2, 
                    Name = "Vilnius"
                }
            );

            modelBuilder.Entity<Tax>().HasData(
                new Tax {
                    Id = 1,
                    Rate = 0.4,
                    TaxType = TaxType.Daily,
                    From = DateTime.Parse("2016.01.01"),
                    MunicipalityId = 1
                },
                new Tax {
                    Id = 2,
                    Rate = 0.2,
                    TaxType = TaxType.Annually,
                    From = DateTime.Parse("2016.01.01"),
                    To = DateTime.Parse("2016.12.31"),
                    MunicipalityId = 1
                },
                new Tax {
                    Id = 3,
                    Rate = 0.2,
                    TaxType = TaxType.Annually,
                    From = DateTime.Parse("2016.01.01"),
                    To = DateTime.Parse("2016.12.31"),
                    MunicipalityId = 2
                }
            );

            //SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Municipality>().HasData(
                new Municipality {
                    Id = 1, 
                    Name = "Copenhagen"

                },
                new Municipality {
                    Id = 2, 
                    Name = "Vilnius"
                }
            );

            modelBuilder.Entity<Tax>().HasData(
                new Tax {
                    Id = 1,
                    Rate = 0.4,
                    TaxType = TaxType.Daily,
                    From = DateTime.Parse("2016.01.01"),
                    MunicipalityId = 1
                },
                new Tax {
                    Id = 2,
                    Rate = 0.2,
                    TaxType = TaxType.Annually,
                    From = DateTime.Parse("2016.01.01"),
                    To = DateTime.Parse("2016.12.31"),
                    MunicipalityId = 1
                },
                new Tax {
                    Id = 3,
                    Rate = 0.2,
                    TaxType = TaxType.Annually,
                    From = DateTime.Parse("2016.01.01"),
                    To = DateTime.Parse("2016.12.31"),
                    MunicipalityId = 2
                }
            );
        }
    }
}
