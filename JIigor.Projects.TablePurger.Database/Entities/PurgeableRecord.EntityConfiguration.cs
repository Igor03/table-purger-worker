using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JIigor.Projects.TablePurger.Database.Entities
{
    public partial class PurgeableRecord
    {
        internal class EntityConfiguration : IEntityTypeConfiguration<PurgeableRecord>
        {
            public void Configure(EntityTypeBuilder<PurgeableRecord> builder)
            {
                _ = builder.ToTable("PURGEABLE_TABLE")
                    .HasKey(pk => pk.Id);

                _ = builder.Property(p => p.Id)
                    .HasColumnName("ID")
                    .HasColumnType("INT")
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                _ = builder.Property(p => p.Name)
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();

                _ = builder.Property(p => p.CreationDate)
                    .HasColumnName("CREATION_DATE")
                    .HasColumnType("DATETIME")
                    .IsRequired();
            }
        }
    }
}
