using LibrarianApplication.Blazor.Model;
using LibraryApp.Contract;
using System.Net.Http.Json;

namespace LibrarianApplication.Blazor.Services
{
    public class BookService : IBookService
    {

        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBookAsync(Book book) => await _httpClient.PostAsJsonAsync($"Books", book);

        public async Task DeleteBookAsync(int id) => await _httpClient.DeleteAsync($"Books/{id}");

        public async Task<IEnumerable<Book>?> GetAllBookAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("Books");
        public async Task<IEnumerable<Book>?> GetAllAvailableBookAsync()
        {
            IEnumerable<Book>? books = await GetAllBookAsync();
            List<Book>? booksIn = new List<Book>();
            foreach (var book in books)
            {
                BookInfo bookStat = await GetBookByIdAsync(book.InventoryNumber);
                if (bookStat.Status.Equals("In")) booksIn.Add(book);
            }

            return booksIn;
        }

        public async Task<BookInfo?> GetBookByIdAsync(int id) => await _httpClient.GetFromJsonAsync<BookInfo?>($"Books/GetById/{id}");

        public async Task UpdateBookAsync(int id, Book book) => await _httpClient.PutAsJsonAsync($"Books/{id}", book);
    }
}
