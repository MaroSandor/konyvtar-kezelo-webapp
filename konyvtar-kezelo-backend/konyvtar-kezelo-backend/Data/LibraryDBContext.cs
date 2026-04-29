namespace konyvtar_kezelo_backend.Data;

using Microsoft.EntityFrameworkCore;
using konyvtar_kezelo_backend.Models;

public class LibraryDBContext : DbContext
{
    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) {}
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Borrowing>()
            .HasOne(b =>  b.Book)
            .WithMany(b => b.Borrowings)
            .HasForeignKey(b => b.BookId);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Reader)
            .WithMany(b => b.Borrowings)
            .HasForeignKey(b => b.ReaderId);
    }
}