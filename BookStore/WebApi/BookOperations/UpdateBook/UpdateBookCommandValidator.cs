using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
  public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
  {
    public UpdateBookCommandValidator()
    {
      RuleFor(command => command.BookId).GreaterThan(0);
      RuleFor(command => command.updatedBook.GenreId).GreaterThan(0).NotEmpty();
      RuleFor(command => command.updatedBook.Title).NotEmpty().MinimumLength(4);
    }
  }
}