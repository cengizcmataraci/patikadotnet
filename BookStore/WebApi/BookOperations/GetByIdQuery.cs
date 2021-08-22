using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
  public class GetByIdQuery
  {
    private readonly BookStoreDbContext _dbContext;
    public GetByIdQuery(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Book Handle(int id)
    {
      var book = _dbContext.Books.Where(book => book.Id == id).SingleOrDefault();

      if (book is null)
        throw new InvalidOperationException("Kitap bulunamadı!");
      return book;
    }
  }
}