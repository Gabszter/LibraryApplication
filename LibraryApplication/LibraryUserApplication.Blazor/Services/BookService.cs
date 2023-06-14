// <copyright file="BookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibraryUserApplication.Blazor.Model;
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
        public async Task<IEnumerable<Book>?> GetAllBookAsync() => await this.httpClient.GetFromJsonAsync<IEnumerable<Book>>("Books");

        /// <summary>
        /// Gets a BookInfo of a Book by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The <see cref="BookInfo"/> of the requested book.</returns>
        public async Task<BookInfo?> GetBookByIdAsync(int id) => await this.httpClient.GetFromJsonAsync<BookInfo?>($"Books/GetById/{id}");
    }
}
