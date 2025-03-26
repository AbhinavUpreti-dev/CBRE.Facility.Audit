using CBRE.FacilityManagement.Audit.Core.WebQuote;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts
{
    public class WebQuoteDatabaseContext : DbContext
    {
        public WebQuoteDatabaseContext(DbContextOptions<WebQuoteDatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
                optionsBuilder.EnableDetailedErrors();
                //optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVERLOCAL;Database=DEV_ELogBooks;Trusted_Connection=True;TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer("Server=SQLQATDNS_WebQuote.emea.cbre.net;Database=QAT_WebQuote;Trusted_Connection=True;TrustServerCertificate=True;", sqlOptions => sqlOptions.CommandTimeout(180));
            }
        }

        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }


        public DbSet<QuoteEstimate> QuoteEstimate { get; set; }
        public DbSet<QuoteQuoteType> Quote_QuoteType { get; set; }
        public DbSet<CoreGroup> Core_Group { get; set; }
        public DbSet<CoreSubGroup> Core_SubGroup { get; set; }
        public DbSet<QuoteEstimateStatus> Quote_EstimateStatus { get; set; }
        public DbSet<CoreClient> Core_Client { get; set; }
        public DbSet<CoreContract> Core_Contract { get; set; }
        public DbSet<CoreContractLocation> Core_ContractLocation { get; set; }
        public DbSet<CoreLocation> Core_Location { get; set; }
        public DbSet<QuoteRequest> Quote_Request { get; set; }
        public DbSet<CoreCurrency> Core_Currency { get; set; }
        public DbSet<QuoteWorksCategory> Quote_WorksCategorie { get; set; }
        public DbSet<CoreStaff> Core_Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebQuoteDatabaseContext).Assembly);

            // Configure relationships and keys if necessary
            modelBuilder.Entity<QuoteEstimate>()
                .ToTable("QUOTE_Estimate")
                .HasOne(qe => qe.QuoteType)
                .WithMany()
                .HasForeignKey(qe => qe.QuoteTypeId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Group)
                .WithMany()
                .HasForeignKey(qe => qe.GroupId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.SubGroup)
                .WithMany()
                .HasForeignKey(qe => qe.SubGroupId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Status)
                .WithMany()
                .HasForeignKey(qe => qe.StatusCodeId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Client)
                .WithMany()
                .HasForeignKey(qe => qe.ClientId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Contract)
                .WithMany()
                .HasForeignKey(qe => qe.ContractId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.ContractLocation)
                .WithMany()
                .HasForeignKey(qe => qe.ContractLocationId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Request)
                .WithMany()
                .HasForeignKey(qe => qe.RequestId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.Currency)
                .WithMany()
                .HasForeignKey(qe => qe.CurrencyId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.WorksCategory)
                .WithMany()
                .HasForeignKey(qe => qe.WorksCategoryId);

            modelBuilder.Entity<QuoteEstimate>()
                .HasOne(qe => qe.InputByStaff)
                .WithMany()
                .HasForeignKey(qe => qe.InputByStaffId);
        }
    }
}
