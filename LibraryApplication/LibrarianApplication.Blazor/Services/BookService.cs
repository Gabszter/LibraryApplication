// <copyright file="BookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibrarianApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for interacting with the book API.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for API requests.</param>
        public BookService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Adds a book asynchronously.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task AddBookAsync(Book book) => await this.httpClient.PostAsJsonAsync($"Books", book);

        /// <summary>
        /// Deletes a book asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task DeleteBookAsync(int id) => await this.httpClient.DeleteAsync($"Books/{id}");

        /// <summary>
        /// Gets all books asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of books.</returns>
        public async Task<IEnumerable<Book>?> GetAllBookAsync() => await this.httpClient.GetFromJsonAsync<IEnumerable<Book>>("Books");

        /// <summary>
        /// Gets all available books asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of available books.</returns>
        public async Task<IEnumerable<Book>?> GetAllAvailableBookAsync()
        {
            IEnumerable<Book>? books = await this.GetAllBookAsync();
            List<Book>? booksIn = new List<Book>();
            if (books is not null)
            {
                foreach (var book in books)
                {
                    if (book is not null)
                    {
                        BookInfo? bookStat = await this.GetBookByIdAsync(book.InventoryNumber);
                        if (bookStat is not null && bookStat.Status is not null)
                        {
                            if (bookStat.Status.Equals("In"))
                            {
                                booksIn.Add(book);
                            }
                        }
                    }
                }
            }

            return booksIn;
        }

        /// <summary>
        /// Gets a BookInfo of a Book by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The <see cref="BookInfo"/> of the requested book.</returns>
        public async Task<BookInfo?> GetBookByIdAsync(int id) => await this.httpClient.GetFromJsonAsync<BookInfo?>($"Books/GetById/{id}");

        /// <summary>
        /// Updates a book asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book object.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task UpdateBookAsync(int id, Book book) => await this.httpClient.PutAsJsonAsync($"Books/{id}", book);
    }
}
