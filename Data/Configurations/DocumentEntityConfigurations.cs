using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.Content)
                .IsRequired()
                .HasColumnType("TEXT");

            builder.Property(d => d.FileSizeInBytes)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(d => d.FileContent)
                .IsRequired()
                .HasColumnType("BYTEA");

        }
    }
}