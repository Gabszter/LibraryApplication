// <copyright file="BorrowService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibrarianApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing borrows.
    /// </summary>
    public class BorrowService : IBorrowService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowService"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for API requests.</param>
        public BorrowService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Adds a borrow asynchronously.
        /// </summary>
        /// <param name="borrow">The borrow object to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task AddBorrowAsync(Borrow borrow) => await this.httpClient.PostAsJsonAsync($"Borrows", borrow);

        /// <summary>
        /// Retrieves borrows by borrower name asynchronously.
        /// </summary>
        /// <param name="name">The name of the borrower.</param>
        /// <returns>An enumerable collection of <see cref="BorrowInfo"/> objects.</returns>
        public async Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name) => await this.httpClient.GetFromJsonAsync<IEnumerable<BorrowInfo>?>($"Borrows/{name}");

        /// <summary>
        /// Retrieves all borrows asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="Borrow"/> objects.</returns>
        public async Task<IEnumerable<Borrow>?> GetAllBorrowsAsync() => await this.httpClient.GetFromJsonAsync<IEnumerable<Borrow>?>("Borrows");

        /// <summary>
        /// Deletes a borrow by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the borrow to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task DeleteBorrowsAsync(int id) => await this.httpClient.DeleteAsync($"Borrows/{id}");
    }
}
