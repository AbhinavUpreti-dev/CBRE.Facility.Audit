using CBRE.FacilityManagement.Audit.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.UniqueId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.IsActive)
                   .IsRequired();

            builder.Property(e => e.Discriminator)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.LoadDate)
                   .IsRequired();

            builder.Property(e => e.UpdateDate)
                   .IsRequired();

            builder.Property(e => e.GlobalCustomerName)
                   .HasMaxLength(100);

            builder.Property(e => e.GlobalCustomerId)
                   .HasMaxLength(50);

            builder.Property(e => e.IsCMT)
                   .IsRequired();
        }
    }
}
