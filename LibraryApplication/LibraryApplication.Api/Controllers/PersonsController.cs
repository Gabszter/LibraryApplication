using LibraryApplication.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace LibraryApplication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(LibraryContext libraryContext, ILogger<PersonsController> logger)
        {
            _libraryContext = libraryContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            _libraryContext.Persons.Add(person);
            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            _logger.LogInformation("People endpoint was called");
            var people = await _libraryContext.Persons.ToListAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
         public async Task<IActionResult> Get(int id)
        {
            var person = await _libraryContext.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            var borrowedBooks = from bw in _libraryContext.Borrows
                           join p in _libraryContext.Persons on bw.ReaderNumber equals p.ReaderNumber
                           join b in _libraryContext.Books on bw.InventoryNumber equals b.InventoryNumber
                           where p.ReaderNumber == id
                           select new
                           {
                               Title = b.Title,
                               ReturnDate = bw.ReturnDate
                           };
            var response = new Dictionary<string, object>
                {
                    { "ReaderNumber", person.ReaderNumber},
                    { "Name", person.Name},
                    { "Address", person.Address},
                    { "BirthDate", person.BirthDate},
                    { "BorrowedBooks", borrowedBooks},
                };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            if (id != person.ReaderNumber)
            {
                return BadRequest();
            }

            var existingPerson = await _libraryContext.Persons.FindAsync(id);

            if (existingPerson is null)
            {
                return NotFound();
            }

            existingPerson.Name = person.Name;
            existingPerson.Address = person.Address;
            existingPerson.BirthDate = person.BirthDate;
            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPerson = await _libraryContext.Persons.FindAsync(id);

            if (existingPerson is null)
            {
                return NotFound();
            }

            _libraryContext.Persons.Remove(existingPerson);
            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
