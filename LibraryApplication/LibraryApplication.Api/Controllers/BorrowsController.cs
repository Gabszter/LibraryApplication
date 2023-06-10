using LibraryApplication.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryApplication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly ILogger<BorrowsController> _logger;

        public BorrowsController(LibraryContext libraryContext, ILogger<BorrowsController> logger)
        {
            _libraryContext = libraryContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Borrow borrow)
        {
            var hasBorrow = await _libraryContext.Borrows
                .Where(br => br.InventoryNumber == borrow.InventoryNumber).AnyAsync();

            if (hasBorrow)
            {
                return BadRequest("This book is borrowed");
            }
            _libraryContext.Borrows.Add(borrow);
            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> Get()
        {
            _logger.LogInformation("Borrow endpoint was called");
            var borrows = await _libraryContext.Borrows.ToListAsync();
            return Ok(borrows);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Borrow>>> Get(string name)
        {

            var person = await _libraryContext.Persons.Where(p => p.Name == name).ToListAsync();
            if (person == null) 
            {
                return NotFound();
            }
            var response = from bw in _libraryContext.Borrows
                                     join p in _libraryContext.Persons on bw.ReaderNumber equals p.ReaderNumber
                                     join b in _libraryContext.Books on bw.InventoryNumber equals b.InventoryNumber
                                     where p.Name == name
                                     select new { Title = b.Title,
                                                  InventoryNumber = b.InventoryNumber,
                                                  BorrowDate = bw.BorrowDate, 
                                                  ReturnDate = bw.ReturnDate };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Borrow borrow)
        {
            if (id != borrow.Id)
            {
                return BadRequest();
            }

            var existingBorrow = await _libraryContext.Borrows.FindAsync(id);

            if (existingBorrow is null)
            {
                return NotFound();
            }

            existingBorrow.ReaderNumber = borrow.ReaderNumber;
            existingBorrow.InventoryNumber=borrow.InventoryNumber;
            existingBorrow.BorrowDate=borrow.BorrowDate;
            existingBorrow.ReturnDate=borrow.ReturnDate;

            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBorrow = await _libraryContext.Borrows.FindAsync(id);

            if (existingBorrow is null)
            {
                return NotFound();
            }

            _libraryContext.Borrows.Remove(existingBorrow);
            await _libraryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
