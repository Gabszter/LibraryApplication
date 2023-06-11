using LibrarianApplication.Blazor.Model;
using LibraryApp.Contract;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBorrowService
    {
        Task AddBorrowAsync(Borrow borrow);
        Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name);
        Task<IEnumerable<Borrow>?> GetAllBorrowsAsync();
        Task DeleteBorrowsAsync(int id);
    }
}
