// <copyright file="BooksController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api.Controllers
{
    using System;
    using System.Data;
    using System.Reflection;
    using System.Xml.Linq;
    using LibraryApp.Contract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    /// <summary>
    /// Represents a controller for managing books in the library application.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<BooksController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="libraryContext">The database context for accessing the library data.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public BooksController(LibraryContext libraryContext, ILogger<BooksController> logger)
        {
            this._libraryContext = libraryContext;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book object to create.</param>
        /// <returns>A response indicating the success of the create operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            this._libraryContext.Books.Add(book);
            await this._libraryContext.SaveChangesAsync();

            return this.Ok();
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A list of all books.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            this._logger.LogInformation("Book endpoint was called");
            var books = await this._libraryContext.Books.ToListAsync();
            return this.Ok(books);
        }

        /// <summary>
        /// Retrieves a book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The book information along with its status and borrower, if applicable.</returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var query = from book in this._libraryContext.Books
                        join inventory in this._libraryContext.Books
                            on book.InventoryNumber equals inventory.InventoryNumber
                        join borrow in this._libraryContext.Borrows
                            on inventory.InventoryNumber equals borrow.InventoryNumber into borrows
                        from borrow in borrows.DefaultIfEmpty()
                        join person in this._libraryContext.Persons
                            on borrow.ReaderNumber equals person.ReaderNumber into persons
                        from person in persons.DefaultIfEmpty()
                        where book.InventoryNumber == id
                        select new
                        {
                            Title = book.Title,
                            Status = borrow == null ? "In" : "Borrowed",
                            Borrower = person == null ? string.Empty : person.Name,
                            DueDate = borrow == null ? string.Empty : borrow.ReturnDate.ToString(),
                        };
            var response = await query.FirstOrDefaultAsync();
            if (response == null)
            {
                return this.NotFound();
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Retrieves a book by title.
        /// </summary>
        /// <param name="title">The title of the book to retrieve.</param>
        /// <returns>The book information along with its status and borrower, if applicable.</returns>
        [HttpGet("[action]/{title}")]
        public async Task<ActionResult> GetByTitle(string title)
        {
            var book = await this._libraryContext.Books.Where(b => b.Title == title).ToListAsync();
            if (book == null)
            {
                return this.NotFound();
            }

            var query = from b in this._libraryContext.Books
                        join inventory in this._libraryContext.Books
                            on b.InventoryNumber equals inventory.InventoryNumber
                        join borrow in this._libraryContext.Borrows
                            on inventory.InventoryNumber equals borrow.InventoryNumber into borrows
                        from borrow in borrows.DefaultIfEmpty()
                        join person in this._libraryContext.Persons
                            on borrow.ReaderNumber equals person.ReaderNumber into persons
                        from person in persons.DefaultIfEmpty()
                        where b.Title == title
                        select new
                        {
                            Title = b.Title,
                            Status = borrow == null ? "In" : "Borrowed",
                            Borrower = person == null ? string.Empty : person.Name,
                            DueDate = borrow == null ? string.Empty : borrow.ReturnDate.ToString(),
                        };

            var response = await query.FirstOrDefaultAsync();
            if (response == null)
            {
                return this.NotFound();
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Updates a book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book object.</param>
        /// <returns>A response indicating the success of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            if (id != book.InventoryNumber)
            {
                return this.BadRequest();
            }

            var existingBook = await this._libraryContext.Books.FindAsync(id);

            if (existingBook is null)
            {
                return this.NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Publisher = book.Publisher;
            existingBook.Author = book.Author;
            existingBook.EditionYear = book.EditionYear;
            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }

        /// <summary>
        /// Deletes a book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>A response indicating the success of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBook = await this._libraryContext.Books.FindAsync(id);

            if (existingBook is null)
            {
                return this.NotFound();
            }

            this._libraryContext.Books.Remove(existingBook);
            await this._libraryContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
