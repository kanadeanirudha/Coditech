
using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public class GeneralDepartmentMasterDBContext : DbContext
    {
        public GeneralDepartmentMasterDBContext(DbContextOptions<GeneralDepartmentMasterDBContext> options) : base(options)
        {
        }
        public DbSet<GeneralDepartmentMaster> GeneralDepartmentMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralDepartmentMaster>(entity =>
            {
                entity.HasKey(e => e.GeneralDepartmentMasterId);
            });
        }
    }
}
