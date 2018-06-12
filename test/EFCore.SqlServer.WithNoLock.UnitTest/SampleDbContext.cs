using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            if (LogMessages == null)
            {
                LogMessages = new List<string>();
                this.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            }
        }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "10.0.2.229",
                InitialCatalog = "Heap_Record",
                UserID = "sa",
                Password = "w1!"
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

        public override int SaveChanges()
        {
            LogMessages.Clear();

            return base.SaveChanges();
        }
        public static IList<string> LogMessages;

        private class MyLoggerProvider : ILoggerProvider
        {
            public ILogger CreateLogger(string categoryName) => new SampleLogger();

            public void Dispose() { }

            private class SampleLogger : ILogger
            {
                public bool IsEnabled(LogLevel logLevel) => true;

                public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                    Func<TState, Exception, string> formatter)
                {
                    if (eventId.Id == RelationalEventId.CommandExecuting.Id)
                    {
                        var message = formatter(state, exception);
                       
                         LogMessages.Add(message);
      
                    }
                }

                public IDisposable BeginScope<TState>(TState state) => null;
            }
        }
    }
}
