using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = CBRE.FacilityManagement.Audit.Core.ElogBook.Action;

namespace CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook
{
    public class ActionConfiguration : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.ToTable("Actions");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.RequiredDate).IsRequired(false);
            builder.Property(a => a.ClosedDate).IsRequired(false);

            builder.HasOne(a => a.DocumentGroup)
                   .WithMany(dg => dg.Actions)
                   .HasForeignKey(a => a.DocumentGroupId);
        }
    }
}
