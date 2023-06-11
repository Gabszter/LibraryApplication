using LibraryApp.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryApplication.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<BooksController> _logger;

        public BooksController(LibraryContext libraryContext, ILogger<BooksController> logger)
        {
            _libraryContext = libraryContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            _logger.LogInformation("Book endpoint was called");
            var books = await _libraryContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var query = from book in _libraryContext.Books
                        join inventory in _libraryContext.Books
                            on book.InventoryNumber equals inventory.InventoryNumber
                        join borrow in _libraryContext.Borrows
                            on inventory.InventoryNumber equals borrow.InventoryNumber into borrows
                        from borrow in borrows.DefaultIfEmpty()
                        join person in _libraryContext.Persons
                            on borrow.ReaderNumber equals person.ReaderNumber into persons
                        from person in persons.DefaultIfEmpty()
                        where book.InventoryNumber == id
                        select new
                        {
                            Title = book.Title,
                            Status = borrow == null ? "In" : "Borrowed",
                            Borrower = person == null ? "" : person.Name,
                            DueDate = borrow == null ? "" : borrow.ReturnDate.ToString()
                        };


            var response = await query.FirstOrDefaultAsync();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpGet("[action]/{title}")]
        public async Task<ActionResult> GetByTitle(string title)
        {
            var book = await _libraryContext.Books.Where(b => b.Title == title).ToListAsync();
            if (book == null)
            {
                return NotFound();
            }
         
            var query = from b in _libraryContext.Books
                        join inventory in _libraryContext.Books
                            on b.InventoryNumber equals inventory.InventoryNumber
                        join borrow in _libraryContext.Borrows
                            on inventory.InventoryNumber equals borrow.InventoryNumber into borrows
                        from borrow in borrows.DefaultIfEmpty()
                        join person in _libraryContext.Persons
                            on borrow.ReaderNumber equals person.ReaderNumber into persons
                        from person in persons.DefaultIfEmpty()
                        where b.Title == title
                        select new
                        {
                            Title = b.Title,
                            Status = borrow == null ? "In" : "Borrowed",
                            Borrower = person == null ? "" : person.Name,
                            DueDate = borrow == null ? "" : borrow.ReturnDate.ToString()
                        };

            var response = await query.FirstOrDefaultAsync();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            if (id != book.InventoryNumber)
            {
                return BadRequest();
            }

            var existingBook = await _libraryContext.Books.FindAsync(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Publisher = book.Publisher;
            existingBook.Author = book.Author;
            existingBook.EditionYear = book.EditionYear;
            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBook = await _libraryContext.Books.FindAsync(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            _libraryContext.Books.Remove(existingBook);
            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
