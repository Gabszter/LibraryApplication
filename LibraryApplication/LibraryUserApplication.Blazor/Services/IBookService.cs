// <copyright file="IBookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using LibraryUserApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing books.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of books.</returns>
        Task<IEnumerable<Book>?> GetAllBookAsync();

        /// <summary>
        /// Gets a BookInfo of a Book by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The <see cref="BookInfo"/> of the requested book.</returns>
        Task<BookInfo?> GetBookByIdAsync(int id);

    }
}
