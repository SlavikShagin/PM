using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Database;

namespace ProjectManager.EntityFramework.Config
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<DeveloperEntity>
    {
        public void Configure(EntityTypeBuilder<DeveloperEntity> builder)
        {
            builder.ToTable("Developers").HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.EMail).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
        }
    }
}
