
using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public class OrganisationDBContext : DbContext
    {
        public OrganisationDBContext(DbContextOptions<OrganisationDBContext> options) : base(options)
        {
        }
        public DbSet<OrganisationMaster> OrganisationMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganisationMaster>(entity =>
            {
                entity.HasKey(e => e.OrganisationMasterId);
            });
        }
    }
}
