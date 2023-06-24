using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public class CoditechDbContext : DbContext
    {

        protected CoditechDbContext() : base()
        {
        }

        public CoditechDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Conventions.Add(_ => new CoditechTriggerConvention());
        //}
    }
}
