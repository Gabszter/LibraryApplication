using LibrarianApplication.Blazor.Model;
using System.Net.Http;
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
        public async Task<IEnumerable<Book>?> GetAllBooksInAsync()
        {
            IEnumerable<Book>? books = await GetAllBookAsync();
            List<Book>? booksIn = new List<Book>();
            foreach (var book in books)
            {
                Book bookTmp = await GetBookByIdAsync(book.InventoryNumber);
                if (bookTmp.Status.Equals("In")) booksIn.Add(book);
            }

            return booksIn;
        }

        public async Task<Book?> GetBookByIdAsync(int id) => await _httpClient.GetFromJsonAsync<Book?>($"Books/GetById/{id}");

        public async Task UpdateBookAsync(int id, Book book) => await _httpClient.PutAsJsonAsync($"Books/{id}", book);
    }
}
