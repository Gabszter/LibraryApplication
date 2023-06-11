using LibrarianApplication.Blazor.Model;
using LibraryApp.Contract;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBookService
    {
        Task AddBookAsync(Book book);

        Task DeleteBookAsync(int id);

        Task<IEnumerable<Book>?> GetAllBookAsync();
        Task<IEnumerable<Book>?> GetAllAvailableBookAsync();

        Task<BookInfo?> GetBookByIdAsync(int id);

        Task UpdateBookAsync(int id, Book book);

    }
}
