using Microsoft.EntityFrameworkCore;

namespace TaxManager.Persistence
{
    public static class TaxManagerContextExtentions {
        
        public static void Migrate(this TaxManagerContext context) {
            context.Database.Migrate();
        }
    }
}