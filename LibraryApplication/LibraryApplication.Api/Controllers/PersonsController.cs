// <copyright file="PersonsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api.Controllers
{
    using System.Xml.Linq;
    using LibraryApp.Contract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a controller for managing persons in the library application.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<PersonsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="libraryContext">The database context for accessing the library data.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PersonsController(LibraryContext libraryContext, ILogger<PersonsController> logger)
        {
            this._libraryContext = libraryContext;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person object to create.</param>
        /// <returns>The created person.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            this._libraryContext.Persons.Add(person);
            await this._libraryContext.SaveChangesAsync();

            return this.Ok();
        }

        /// <summary>
        /// Retrieves all persons.
        /// </summary>
        /// <returns>A list of all persons.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            this._logger.LogInformation("People endpoint was called");
            var people = await this._libraryContext.Persons.ToListAsync();
            return this.Ok(people);
        }

        /// <summary>
        /// Retrieves a person by ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>The person with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await this._libraryContext.Persons.FindAsync(id);
            if (person == null)
            {
                return this.NotFound();
            }

            var borrowedBooks = from bw in this._libraryContext.Borrows
                           join p in this._libraryContext.Persons on bw.ReaderNumber equals p.ReaderNumber
                           join b in this._libraryContext.Books on bw.InventoryNumber equals b.InventoryNumber
                           where p.ReaderNumber == id
                           select new
                           {
                               Title = b.Title,
                               ReturnDate = bw.ReturnDate,
                           };
            var response = new Dictionary<string, object>
                {
                    { "ReaderNumber", person.ReaderNumber },
                    { "Name", person.Name },
                    { "Address", person.Address },
                    { "BirthDate", person.BirthDate },
                    { "BorrowedBooks", borrowedBooks },
                };
            return this.Ok(response);
        }

        /// <summary>
        /// Updates a person's information.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="person">The updated person object.</param>
        /// <returns>A response indicating the success of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            if (id != person.ReaderNumber)
            {
                return this.BadRequest();
            }

            var existingPerson = await this._libraryContext.Persons.FindAsync(id);

            if (existingPerson is null)
            {
                return this.NotFound();
            }

            existingPerson.Name = person.Name;
            existingPerson.Address = person.Address;
            existingPerson.BirthDate = person.BirthDate;
            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// Deletes a person by ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <returns>A response indicating the success of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPerson = await this._libraryContext.Persons.FindAsync(id);

            if (existingPerson is null)
            {
                return this.NotFound();
            }

            this._libraryContext.Persons.Remove(existingPerson);
            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
