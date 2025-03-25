using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.Configurations.ElogBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts
{
    public class ELogBookDbContext : DbContext
    {
        public ELogBookDbContext(DbContextOptions<ELogBookDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<DocumentGroup> DocumentGroups { get; set; }
        public DbSet<DocumentGroupProperty> DocumentGroupProperties { get; set; }
        public DbSet<ContractBuilding> ContractBuildings { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Building> Buildings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVERLOCAL;Database=DEV_ELogBooks;Trusted_Connection=True;TrustServerCertificate=True;");
                //optionsBuilder.UseSqlServer("Server=SQLQATDNS_Elogbooks.emea.cbre.net;Database=QAT_ELogBooks;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply the Customer configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ELogBookDbContext).Assembly);

            // Configure relationships
            modelBuilder.Entity<Documents>()
                .HasOne(d => d.DocumentGroup)
                .WithMany(dg => dg.Documents)
                .HasForeignKey(d => d.DocumentGroupId);

            modelBuilder.Entity<DocumentGroupProperty>()
                .HasOne(dgp => dgp.DocumentGroup)
                .WithMany(dg => dg.DocumentGroupProperties)
                .HasForeignKey(dgp => dgp.DocumentGroupId);

            modelBuilder.Entity<DocumentGroupProperty>()
                .HasOne(dgp => dgp.ContractBuilding)
                .WithMany(cb => cb.DocumentGroupProperties)
                .HasForeignKey(dgp => dgp.BuildingId);

            modelBuilder.Entity<ContractBuilding>()
                .HasOne(cb => cb.Contract)
                .WithMany(c => c.ContractBuildings)
                .HasForeignKey(cb => cb.ContractId);

            modelBuilder.Entity<ContractBuilding>()
                .HasOne(cb => cb.Building)
                .WithMany(b => b.ContractBuildings)
                .HasForeignKey(cb => cb.BuildingId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Customer)
                .WithMany(cust => cust.Contracts)
                .HasForeignKey(c => c.CustomerId);
        }
    }
}
