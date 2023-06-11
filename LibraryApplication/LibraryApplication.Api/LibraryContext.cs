using Microsoft.EntityFrameworkCore;
using LibraryApp.Contract;

namespace LibraryApplication.Api
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }

        public virtual DbSet<Book> Books { get; set; }
    }
}
