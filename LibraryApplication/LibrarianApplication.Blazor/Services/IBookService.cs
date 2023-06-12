// <copyright file="IBookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
{
    using LibrarianApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing books.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Adds a book asynchronously.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task AddBookAsync(Book book);

        /// <summary>
        /// Deletes a book asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task DeleteBookAsync(int id);

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of books.</returns>
        Task<IEnumerable<Book>?> GetAllBookAsync();

        /// <summary>
        /// Retrieves all available books asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of available books.</returns>
        Task<IEnumerable<Book>?> GetAllAvailableBookAsync();

        /// <summary>
        /// Gets a BookInfo of a Book by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The <see cref="BookInfo"/> of the requested book.</returns>
        Task<BookInfo?> GetBookByIdAsync(int id);

        /// <summary>
        /// Updates a book asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book object.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task UpdateBookAsync(int id, Book book);
    }
}
