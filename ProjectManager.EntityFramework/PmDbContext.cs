using Microsoft.EntityFrameworkCore;
using ProjectManager.Database;
using ProjectManager.EntityFramework.Config;

namespace ProjectManager.EntityFramework
{
    public class PmDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<DeveloperEntity> Developers { get; set; }

        public PmDbContext(DbContextOptions<PmDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
        }
    }
}
