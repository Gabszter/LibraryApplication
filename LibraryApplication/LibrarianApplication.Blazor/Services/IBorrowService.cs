using LibrarianApplication.Blazor.Model;

namespace LibrarianApplication.Blazor.Services
{
    public interface IBorrowService
    {
        Task AddBorrowAsync(Borrow borrow);
    }
}
