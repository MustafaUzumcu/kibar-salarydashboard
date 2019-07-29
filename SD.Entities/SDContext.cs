using Microsoft.EntityFrameworkCore;
using SD.Entities.Concrete;

namespace SD.Entities
{
    public class SDContext : DbContext
    {
        public SDContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemParameter>().HasData(new SystemParameter
            {
                ParameterId = 1,
                ParameterName = "SYSTEMSTATUS",
                ParameterValue = "ACTIVE",
                Description = "Sistemin açık olup olmadığı durumu",
                IsReadOnly = true
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SystemParameter> SystemParameters { get; set; }
    }
}
