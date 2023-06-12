// <copyright file="BookInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Model
{
    /// <summary>
    /// The status info of a requested Book.
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// Gets or sets the title of the requested book.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the borrow status of the requested book.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the requested book borrower.
        /// </summary>
        public string? Borrower { get; set; }

        /// <summary>
        /// Gets or sets the return date of the requested book.
        /// </summary>
        public string? DueDate { get; set; }
    }
}
