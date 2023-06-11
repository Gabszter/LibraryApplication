
using LibrarianApplication.Blazor.Model;
using System.Net.Http.Json;
using LibraryApp.Contract;

namespace LibrarianApplication.Blazor.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly HttpClient _httpClient;

        public BorrowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBorrowAsync(Borrow borrow) => await _httpClient.PostAsJsonAsync($"Borrows", borrow);

        public async Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name) => await _httpClient.GetFromJsonAsync<IEnumerable<BorrowInfo>?>($"Borrows/{name}");

        public async Task<IEnumerable<Borrow>?> GetAllBorrowsAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Borrow>?>("Borrows");

        public async Task DeleteBorrowsAsync(int id) => await _httpClient.DeleteAsync($"Borrows/{id}");
    }
}
