using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBorrowService
    {
        Task AddBorrowAsync(Borrow borrow);
        Task<IEnumerable<Borrow>?> GetBorrowByName(string name);
    }
}
