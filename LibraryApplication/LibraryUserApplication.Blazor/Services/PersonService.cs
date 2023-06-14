// <copyright file="PersonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing persons.
    /// </summary>
    public class PersonService : IPersonService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used for API requests.</param>
        public PersonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

    }
}
