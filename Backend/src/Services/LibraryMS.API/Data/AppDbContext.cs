using LibraryMS.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Book>()
                    .Property(b => b.BookId)
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<Member>()
                .Property(m => m.MemberId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Library>()
                .Property(m => m.LibraryId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BookLoan>()
                .Property(m => m.BookLoanId)
                .ValueGeneratedOnAdd();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
