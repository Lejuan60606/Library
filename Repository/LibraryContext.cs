using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Repository
{
    public class LibraryContext : DbContext
    {
        private readonly string _connectionString;

        public LibraryContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = new ConfigurationBuilder().Build();

            //optionsBuilder.UseSqlServer(_connectionString, builder =>
            //{
            //    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            //});

            new ConfigurationBuilder().Build();
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<BorrowTransaction> BorrowTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(d => new { d.Id });
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(d => new { d.Id });
            });

            modelBuilder.Entity<BorrowTransaction>(entity =>
            {
                entity.HasKey(d => new { d.Id });
            });
        }
    }
}
