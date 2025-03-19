using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts
{
    public class ELogBookDbContext : DbContext
    {
        public ELogBookDbContext(DbContextOptions<ELogBookDbContext> options) : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVERLOCAL;Database=DEV_ELogBooks;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply the Customer configuration
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        }
    }
}
