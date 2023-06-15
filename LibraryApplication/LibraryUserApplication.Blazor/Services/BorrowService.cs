// <copyright file="BorrowService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibraryUserApplication.Blazor.Model;
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
        /// Retrieves borrows by borrower name asynchronously.
        /// </summary>
        /// <param name="name">The name of the borrower.</param>
        /// <returns>An enumerable collection of <see cref="BorrowInfo"/> objects.</returns>
        public async Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name) => await this.httpClient.GetFromJsonAsync<IEnumerable<BorrowInfo>?>($"Borrows/{name}");

    }
}
