using Microsoft.EntityFrameworkCore;
using SD.Entities.Concrete;

namespace SD.Entities
{
    public class SDContext : DbContext
    {
        public SDContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SystemParameter> SystemParameters { get; set; }
    }
}
