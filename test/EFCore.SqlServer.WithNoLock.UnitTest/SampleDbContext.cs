using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace EFCore.SqlServer.WithNoLock.UnitTest
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext()
        {
        }

        public SampleDbContext(ITestOutputHelper testOutputHelper)
        {
            var serviceProvider = this.GetInfrastructure();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new MyLoggerProvider(testOutputHelper));
        }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "10.0.2.229",
                InitialCatalog = "Heap_Record",
                UserID = "sa",
                Password = ""
            };

            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            optionsBuilder.UseSqlServerWithNoLock();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            EntityTypeBuilder<Category> entityTypeBuilder = modelBuilder.Entity<Category>();
            entityTypeBuilder.ToTable("Category");
            entityTypeBuilder.HasKey(e => e.CategoryID);
            entityTypeBuilder.Property(e => e.CategoryID);
       
        }
    }
}