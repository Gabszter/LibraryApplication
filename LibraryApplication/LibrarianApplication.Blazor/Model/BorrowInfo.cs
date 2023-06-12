// <copyright file="BorrowInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibrarianApplication.Blazor.Model
{
    /// <summary>
    /// The info of a requested Borrow.
    /// </summary>
    public class BorrowInfo
    {
        /// <summary>
        /// Gets or sets the title of the book in the requested Borrow.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the InventoryNumber of the book in the requested Borrow.
        /// </summary>
        public int InventoryNumber { get; set; }

        /// <summary>
        /// Gets or sets the borrow date of the requested Borrow.
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Gets or sets the return date of the requested Borrow.
        /// </summary>
        public DateTime ReturnDate { get; set; }
    }
}
