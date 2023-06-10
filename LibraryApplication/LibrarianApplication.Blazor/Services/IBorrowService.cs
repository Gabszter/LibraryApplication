using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBorrowService
    {
        Task AddBorrowAsync(Borrow borrow);
        Task<IEnumerable<Borrow>?> GetBorrowByNameAsync(string name);
        Task<IEnumerable<Borrow>?> GetAllBorrowsAsync();
        Task DeleteBorrowsAsync(int id);
    }
}
