using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Persistence
{
    public class ConnectedContext : DbContext
    {
        public ConnectedContext(DbContextOptions options):base(options)
        {
            
        }
    }
}