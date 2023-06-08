using LibrarianApplication.Blazor.Model;
using System.Net.Http.Json;

namespace LibrarianApplication.Blazor.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Person>?> GetAllPeopleAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Person>>("Persons");

        public async Task<Person?> GetPeopleByIdAsync(int id) => await _httpClient.GetFromJsonAsync<Person?>($"Persons/{id}");

        public async Task UpdatePersonAsync(int id, Person person) => await _httpClient.PutAsJsonAsync($"Persons/{id}", person);

        public async Task DeletePersonAsync(int id) => await _httpClient.DeleteAsync($"Persons/{id}");

        public async Task AddPersonAsync(Person person) => await _httpClient.PostAsJsonAsync($"Persons", person);
    }
}
