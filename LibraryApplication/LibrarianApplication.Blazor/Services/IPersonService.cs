using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IPersonService
    {
        Task AddPersonAsync(Person person);

        Task DeletePersonAsync(int id);

        Task<IEnumerable<Person>?> GetAllPersonAsync();

        Task<Person?> GetPersonByIdAsync(int id);

        Task UpdatePersonAsync(int id, Person person);

    }
}
