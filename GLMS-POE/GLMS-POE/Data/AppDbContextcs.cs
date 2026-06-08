using Microsoft.EntityFrameworkCore;
using GLMS_POE.Models;

namespace GLMS_POE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////