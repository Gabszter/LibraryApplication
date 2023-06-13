namespace LibraryApplication.Api.Controllers
{
    using System;
    using LibraryApp.Contract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<BorrowsController> _logger;

        public BorrowsController(LibraryContext libraryContext, ILogger<BorrowsController> logger)
        {
            this._libraryContext = libraryContext;
            this._logger = logger;
        }

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> Get()
        {
            this._logger.LogInformation("Borrow endpoint was called");
            var borrows = await this._libraryContext.Borrows.ToListAsync();
            return this.Ok(borrows);
        }

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
