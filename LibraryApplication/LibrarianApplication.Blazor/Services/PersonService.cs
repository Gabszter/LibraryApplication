// <copyright file="PersonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
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

        /// <summary>
        /// Adds a person asynchronously.
        /// </summary>
        /// <param name="person">The person to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task AddPersonAsync(Person person) => await this.httpClient.PostAsJsonAsync($"Persons", person);

        /// <summary>
        /// Deletes a person asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task DeletePersonAsync(int id) => await this.httpClient.DeleteAsync($"Persons/{id}");

        /// <summary>
        /// Retrieves all persons asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of persons.</returns>
        public async Task<IEnumerable<Person>?> GetAllPersonAsync() => await this.httpClient.GetFromJsonAsync<IEnumerable<Person>>("Persons");

        /// <summary>
        /// Retrieves a person by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>The <see cref="Person"/> object of the requested person.</returns>
        public async Task<Person?> GetPersonByIdAsync(int id) => await this.httpClient.GetFromJsonAsync<Person?>($"Persons/{id}");

        /// <summary>
        /// Updates a person asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="person">The updated person object.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        public async Task UpdatePersonAsync(int id, Person person) => await this.httpClient.PutAsJsonAsync($"Persons/{id}", person);
    }
}
