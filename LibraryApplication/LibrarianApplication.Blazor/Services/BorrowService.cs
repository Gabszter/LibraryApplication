
using LibrarianApplication.Blazor.Model;
using System.Net.Http.Json;

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
    }
}
