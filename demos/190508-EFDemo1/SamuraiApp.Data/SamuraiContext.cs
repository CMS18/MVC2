using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext: DbContext
    {
        private ILoggerFactory MyConsoleLoggerFactory; //<--this is part of the new 2.2 implementation

        public SamuraiContext()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                        .AddConsole()
                        .AddFilter
                        (DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information));

            MyConsoleLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add N-N Relation
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new
                {
                    s.BattleId,
                    s.SamuraiId
                });
        }
    }
}
