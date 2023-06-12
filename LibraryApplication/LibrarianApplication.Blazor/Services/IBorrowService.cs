// <copyright file="IBorrowService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
{
    using LibrarianApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing borrows.
    /// </summary>
    public interface IBorrowService
    {
        /// <summary>
        /// Adds a borrow asynchronously.
        /// </summary>
        /// <param name="borrow">The borrow object to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task AddBorrowAsync(Borrow borrow);

        /// <summary>
        /// Retrieves borrows by borrower name asynchronously.
        /// </summary>
        /// <param name="name">The name of the borrower.</param>
        /// <returns>An enumerable collection of <see cref="BorrowInfo"/> objects.</returns>
        Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name);

        /// <summary>
        /// Retrieves all borrows asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="Borrow"/> objects.</returns>
        Task<IEnumerable<Borrow>?> GetAllBorrowsAsync();

        /// <summary>
        /// Deletes a borrow by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the borrow to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task DeleteBorrowsAsync(int id);
    }
}
