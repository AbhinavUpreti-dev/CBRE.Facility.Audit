using CBRE.FacilityManagement.Audit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook
{
    public class DocumentGroupConfiguration : IEntityTypeConfiguration<DocumentGroup>
    {
        public void Configure(EntityTypeBuilder<DocumentGroup> builder)
        {
            builder.HasKey(dg => dg.Id);

            builder.Property(dg => dg.Name)
                .IsRequired(false) // Allow null or empty
                .HasMaxLength(255);

            builder.HasOne(dg => dg.Parent)
                .WithMany(dg => dg.SubGroups)
                .HasForeignKey(dg => dg.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(dg => dg.Documents)
                .WithOne(d => d.DocumentGroup)
                .HasForeignKey(d => d.DocumentGroupId);

            builder.HasMany(dg => dg.DocumentGroupProperties)
                .WithOne(dgp => dgp.DocumentGroup)
                .HasForeignKey(dgp => dgp.DocumentGroupId);

            builder.HasOne(dg => dg.MasterDocumentGroup)
                .WithMany()
                .HasForeignKey(dg => dg.MasterDocumentGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Specify the table name for MasterDocumentGroup
            builder.ToTable("DocumentGroups");
        }
    }
}


