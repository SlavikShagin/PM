using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Database;

namespace ProjectManager.EntityFramework.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasMany(x => x.Developers).WithMany(x => x.Projects);

            builder.ToTable("Projects").HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ProjectSubject).IsRequired();
        }
    }
}
