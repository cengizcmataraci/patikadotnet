using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations
{
  public class BookStoreDbContext : DbContext
  {
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    { }
    public DbSet<Book> Books { get; set; } //entity isimleri tekil, dbde yaratılacak isim çoğul olur
  }
}