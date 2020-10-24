using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaxManager.Persistence {

    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<TaxManagerContext> 
    { 
        public TaxManagerContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($@"{Directory.GetCurrentDirectory()}/../TaxManager.WebApi/appsettings.json")
                .Build(); 
            var builder = new DbContextOptionsBuilder<TaxManagerContext>(); 
            var connectionString = configuration.GetConnectionString("TaxManager"); 
            builder.UseSqlite<TaxManagerContext>(connectionString); 
            return new TaxManagerContext(builder.Options); 
        } 
    }
}