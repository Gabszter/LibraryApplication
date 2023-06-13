// <copyright file="LibraryContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api
{
    using LibraryApp.Contract;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the database context for the library application.
    /// </summary>
    public class LibraryContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public LibraryContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the collection of persons in the database.
        /// </summary>
        public virtual DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Gets or sets the collection of borrows in the database.
        /// </summary>
        public virtual DbSet<Borrow> Borrows { get; set; }

        /// <summary>
        /// Gets or sets the collection of books in the database.
        /// </summary>
        public virtual DbSet<Book> Books { get; set; }
    }
}
