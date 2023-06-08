using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>?> GetAllPeopleAsync();

        Task<Person?> GetPeopleByIdAsync(int id);

        Task UpdatePersonAsync(int id, Person person);

        Task DeletePersonAsync(int id);
        Task AddPersonAsync(Person person);
    }
}
