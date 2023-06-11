using LibrarianApplication.Blazor.Model;
using System.Net.Http.Json;
using LibraryApp.Contract;

namespace LibrarianApplication.Blazor.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddPersonAsync(Person person) => await _httpClient.PostAsJsonAsync($"Persons", person);

        public async Task DeletePersonAsync(int id) => await _httpClient.DeleteAsync($"Persons/{id}");

        public async Task<IEnumerable<Person>?> GetAllPersonAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Person>>("Persons");

        public async Task<Person?> GetPersonByIdAsync(int id) => await _httpClient.GetFromJsonAsync<Person?>($"Persons/{id}");

        public async Task UpdatePersonAsync(int id, Person person) => await _httpClient.PutAsJsonAsync($"Persons/{id}", person);
    }
}
