using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public partial class Coditech_Entities : CoditechDbContext
    {
        public Coditech_Entities()
        {
        }

        public Coditech_Entities(DbContextOptions<Coditech_Entities> options)
            : base(options)
        {
        }

        #region General Master
        public DbSet<GeneralDepartmentMaster> GeneralDepartmentMaster { get; set; }
        public DbSet<GeneralCountryMaster> GeneralCountryMaster { get; set; }
        public DbSet<GeneralTaxMaster> GeneralTaxMaster { get; set; }
        public DbSet<GeneralTaxGroupMaster> GeneralTaxGroupMaster { get; set; }
        public DbSet<GeneralCityMaster> GeneralCityMaster { get; set; }
        #endregion

        #region Organisation
        public DbSet<OrganisationCentreMaster> OrganisationCentreMaster { get; set; }
        #endregion

        #region Admin
        public DbSet<AdminRoleMenuDetail> AdminRoleMenuDetail { get; set; }
        public DbSet<AdminRoleApplicableDetail> AdminRoleApplicableDetail { get; set; }
        #endregion

        #region User
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<UserModuleMaster> UserModuleMaster { get; set; }
        public DbSet<UserMainMenuMaster> UserMainMenuMaster { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
