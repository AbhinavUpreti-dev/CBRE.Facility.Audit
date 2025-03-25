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
    public class MasterDocumentGroupConfiguration : IEntityTypeConfiguration<MasterDocumentGroup>
    {
        public void Configure(EntityTypeBuilder<MasterDocumentGroup> builder)
        {
            builder.HasKey(mdg => mdg.Id);

            builder.Property(mdg => mdg.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.ToTable("MasterDocumentGroups");
        }
    }
}
