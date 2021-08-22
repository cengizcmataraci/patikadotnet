using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {
    private readonly BookStoreDbContext _context;

    public BookController(BookStoreDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
      GetBooksQuery query = new GetBooksQuery(_context);
      var result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")] // Daha doğru olan yaklaşım
    public IActionResult GetById(int id)
    {
      GetByIdQuery query = new GetByIdQuery(_context);
      try
      {
        var result = query.Handle(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // [HttpGet]
    // public Book Get([FromQuery] string id)
    // {
    //   var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //   return book;
    // }

    //Post
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
      CreateBookCommand command = new CreateBookCommand(_context);
      try
      {
        command.Model = newBook;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();
    }

    //Put
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updated)
    {
      UpdateBookCommand command = new UpdateBookCommand(_context);
      try
      {
        command.updatedBook = updated;
        command.Handle(id);
      }
      catch (Exception ex)
      {

        return BadRequest(ex.Message);
      }
      return Ok();
    }

    //Delete
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
      var book = _context.Books.SingleOrDefault(x => x.Id == id);

      if (book is null)
        return BadRequest();

      _context.Books.Remove(book);

      _context.SaveChanges();

      return Ok();

    }
  }
}