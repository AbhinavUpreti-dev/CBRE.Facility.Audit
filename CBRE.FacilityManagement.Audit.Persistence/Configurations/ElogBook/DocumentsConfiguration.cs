using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBRE.FacilityManagement.Audit.Core.ElogBook;

namespace CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook
{
    public class DocumentsConfiguration : IEntityTypeConfiguration<Documents>
    {
        public void Configure(EntityTypeBuilder<Documents> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EntityId)
                   .IsRequired();

            builder.Property(e => e.DocumentGroupId)
                   .IsRequired();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.MimeType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Extension)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(e => e.Created)
                   .IsRequired();

            builder.Property(e => e.CreatedById)
                   .IsRequired();

            builder.Property(e => e.Modified);

            builder.Property(e => e.ModifiedById);

            builder.Property(e => e.UniqueId)
                   .IsRequired();

            builder.Property(e => e.IsActive)
                   .IsRequired();

            builder.Property(e => e.RowId)
                   .IsRequired();

            builder.Property(e => e.FileData)
                   .IsRequired();

            builder.Property(e => e.LevelTypeId)
                   .IsRequired();

            builder.Property(e => e.AssetId)
                   .IsRequired(false);

            builder.Property(e => e.ScanResult)
                   .HasMaxLength(500);
        }
    }
}
