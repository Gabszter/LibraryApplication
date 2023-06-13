namespace LibraryApplication.Api.Controllers
{
    using System.Xml.Linq;
    using LibraryApp.Contract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(LibraryContext libraryContext, ILogger<PersonsController> logger)
        {
            this._libraryContext = libraryContext;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            this._libraryContext.Persons.Add(person);
            await this._libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            this._logger.LogInformation("People endpoint was called");
            var people = await this._libraryContext.Persons.ToListAsync();
            return this.Ok(people);
        }

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
