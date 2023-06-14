// <copyright file="PersonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using System.Net.Http.Json;
    using LibraryApp.Contract;
    using LibraryUserApplication.Blazor.Model;

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
        /// Retrieves all persons asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of persons.</returns>
        public async Task<IEnumerable<Person>?> GetAllPersonAsync() => await this.httpClient.GetFromJsonAsync<IEnumerable<Person>>("Persons");




        public async Task<IEnumerable<Person>?> GetPersonForQueryAsync(string name)
        {
            IEnumerable<Person>? persons = await this.GetAllPersonAsync();
            List<Person>? searchPerson = new List<Person>();
            if (persons != null && persons.Any())
                foreach (var member in persons)
                {
                    if (name == member.Name)
                    {
                        searchPerson.Add(member);
                    }
                }

            return searchPerson;
        }
    }
}
