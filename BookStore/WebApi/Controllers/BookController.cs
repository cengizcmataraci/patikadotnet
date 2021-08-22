using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
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
    public IActionResult GetBookDetail(int id)
    {
      BookDetailViewModel result;
      try
      {
        GetBookDetailQuery query = new GetBookDetailQuery(_context);
        query.BookId = id;
        result = query.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok(result);
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
      try
      {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.updatedBook = updated;
        command.Handle();
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
      try
      {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = id;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();

    }
  }
}