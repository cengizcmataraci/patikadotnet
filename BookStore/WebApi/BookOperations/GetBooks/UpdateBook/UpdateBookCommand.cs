using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
  public class UpdateBookCommand
  {
    public UpdateBookModel updatedBook { get; set; }
    private readonly BookStoreDbContext _dbContext;
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle(int id)
    {
      var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);

      if (book is null)
        throw new InvalidOperationException("Kitap bulunamadı!");

      book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
      book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
      book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
      book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

      _dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
      public string Title { get; set; }
      public int GenreId { get; set; }
      public int PageCount { get; set; }
      public DateTime PublishDate { get; set; }
    }
  }
}
