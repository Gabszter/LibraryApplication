// <copyright file="Book.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApp.Contract
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the inventory number of the book.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        [Required]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the book.
        /// </summary>
        [Required]
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the edition year of the book.
        /// </summary>
        [Required]
        public int EditionYear { get; set; }
    }
}

