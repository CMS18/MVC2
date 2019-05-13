using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BankAppModel
{
    public partial class BankAppDataContext : DbContext
    {
        private ILoggerFactory MyConsoleLoggerFactory; 


        public BankAppDataContext()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                        .AddConsole()
                        .AddFilter
                        (DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information));

            MyConsoleLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        public BankAppDataContext(DbContextOptions<BankAppDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Disposition> Dispositions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
                    .UseLoggerFactory(MyConsoleLoggerFactory)
                    .EnableSensitiveDataLogging(true)
                    //.UseLazyLoadingProxies()
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankAppData;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK_account");

                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.Frequency)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.ToTable("Accounts");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Emailaddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Givenname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Streetaddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(23);

                entity.Property(e => e.Telephonenumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasMaxLength(15);

                //entity.ToTable("Customers");

            });

            modelBuilder.Entity<Disposition>(entity =>
            {
                entity.HasKey(e => e.DispositionId)
                    .HasName("PK_disposition");

                entity.Property(e => e.DispositionId).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Dispositions)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dispositions_Accounts");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Dispositions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dispositions_Customers");

                entity.ToTable("Dispositions");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId).ValueGeneratedNever();

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.Balance).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.Bank).HasMaxLength(50);

                entity.Property(e => e.Operation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Symbol).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Account");

                entity.ToTable("Transactions");
            });
        }
    }
}
