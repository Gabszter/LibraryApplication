// <copyright file="BorrowsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api.Controllers
{
    using System;
    using LibraryApp.Contract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a controller for managing borrows in the library application.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<BorrowsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowsController"/> class.
        /// </summary>
        /// <param name="libraryContext">The database context for accessing the library data.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public BorrowsController(LibraryContext libraryContext, ILogger<BorrowsController> logger)
        {
            this._libraryContext = libraryContext;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a new borrow entry.
        /// </summary>
        /// <param name="borrow">The borrow object to create.</param>
        /// <returns>A response indicating the success of the create operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Borrow borrow)
        {
            var hasBorrow = await this._libraryContext.Borrows
                .Where(br => br.InventoryNumber == borrow.InventoryNumber).AnyAsync();

            if (hasBorrow)
            {
                return this.BadRequest("This book is borrowed");
            }

            this._libraryContext.Borrows.Add(borrow);
            await this._libraryContext.SaveChangesAsync();

            return this.Ok();
        }

        /// <summary>
        /// Retrieves all borrows.
        /// </summary>
        /// <returns>A list of all borrows.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> Get()
        {
            this._logger.LogInformation("Borrow endpoint was called");
            var borrows = await this._libraryContext.Borrows.ToListAsync();
            return this.Ok(borrows);
        }

        /// <summary>
        /// Retrieves borrows by person's name.
        /// </summary>
        /// <param name="name">The name of the person to retrieve borrows for.</param>
        /// <returns>A list of borrows associated with the person.</returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Borrow>>> Get(string name)
        {
            var person = await this._libraryContext.Persons.Where(p => p.Name == name).ToListAsync();
            if (person == null) 
            {
                return this.NotFound();
            }

            var response = from bw in this._libraryContext.Borrows
                                     join p in this._libraryContext.Persons on bw.ReaderNumber equals p.ReaderNumber
                                     join b in this._libraryContext.Books on bw.InventoryNumber equals b.InventoryNumber
                                     where p.Name == name
                                     select new { Title = b.Title,
                                                  InventoryNumber = b.InventoryNumber,
                                                  BorrowDate = bw.BorrowDate,
                                                  ReturnDate = bw.ReturnDate,
                                     };
            return this.Ok(response);
        }

        /// <summary>
        /// Updates a borrow entry.
        /// </summary>
        /// <param name="id">The ID of the borrow entry to update.</param>
        /// <param name="borrow">The updated borrow object.</param>
        /// <returns>A response indicating the success of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Borrow borrow)
        {
            if (id != borrow.Id)
            {
                return this.BadRequest();
            }

            var existingBorrow = await this._libraryContext.Borrows.FindAsync(id);

            if (existingBorrow is null)
            {
                return this.NotFound();
            }

            existingBorrow.ReaderNumber = borrow.ReaderNumber;
            existingBorrow.InventoryNumber = borrow.InventoryNumber;
            existingBorrow.BorrowDate = borrow.BorrowDate;
            existingBorrow.ReturnDate = borrow.ReturnDate;

            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// Deletes a borrow entry by ID.
        /// </summary>
        /// <param name="id">The ID of the borrow entry to delete.</param>
        /// <returns>A response indicating the success of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBorrow = await this._libraryContext.Borrows.FindAsync(id);

            if (existingBorrow is null)
            {
                return this.NotFound();
            }

            this._libraryContext.Borrows.Remove(existingBorrow);
            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
