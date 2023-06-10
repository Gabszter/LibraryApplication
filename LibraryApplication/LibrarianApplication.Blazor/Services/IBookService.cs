using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBookService
    {
        Task AddBookAsync(Book book);

        Task DeleteBookAsync(int id);

        Task<IEnumerable<Book>?> GetAllBookAsync();
        Task<IEnumerable<Book>?> GetAllBooksInAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task UpdateBookAsync(int id, Book book);

    }
}
