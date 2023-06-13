
using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }
        public DbSet<UserMaster> UserMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserMasterId);
                entity.ToTable("UserMaster");
                entity.Property(e => e.UserType).HasMaxLength(1).IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(100).IsRequired().IsUnicode(true);
                entity.Property(e => e.Password).HasMaxLength(200).IsRequired().IsUnicode(false);
                entity.Property(e => e.EmailId).HasMaxLength(200).IsRequired().IsUnicode(false);
                entity.Property(e => e.FirstName).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.LastName).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.Gender).IsRequired();
            });
        }
    }
}
