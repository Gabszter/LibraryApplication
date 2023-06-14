// <copyright file="IPersonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing persons.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves all persons asynchronously.
        /// </summary>
        /// <returns>An enumerable collection of persons.</returns>
        Task<IEnumerable<Person>?> GetAllPersonAsync();


        Task<IEnumerable<Person>?> GetPersonForQueryAsync(string name);
        
    }
}
