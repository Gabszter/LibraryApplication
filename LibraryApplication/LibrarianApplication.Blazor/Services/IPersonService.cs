// <copyright file="IPersonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Services
{
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing persons.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a person asynchronously.
        /// </summary>
        /// <param name="person">The person to add.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task AddPersonAsync(Person person);

        /// <summary>
        /// Deletes a person asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task DeletePersonAsync(int id);

        /// <summary>
        /// Retrieves all persons asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of persons.</returns>
        Task<IEnumerable<Person>?> GetAllPersonAsync();

        /// <summary>
        /// Retrieves a person by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>The <see cref="Person"/> object of the requested person.</returns>
        Task<Person?> GetPersonByIdAsync(int id);

        /// <summary>
        /// Updates a person asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="person">The updated person object.</param>
        /// <returns>"Task" representing the asynchronous operation. </returns>
        Task UpdatePersonAsync(int id, Person person);
    }
}
