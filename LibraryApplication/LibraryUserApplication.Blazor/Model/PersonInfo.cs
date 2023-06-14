// <copyright file="BorrowInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Model
{
    /// <summary>
    /// The info of a requested Borrow.
    /// </summary>
    public class PersonInfo
    {
        /// <summary>
        /// Gets or sets the title of the book in the requested Borrow.
        /// </summary>
        public int? ReaderNumber { get; set; }

        /// <summary>
        /// Gets or sets the InventoryNumber of the book in the requested Borrow.
        /// </summary>
        public string? Name { get; set; }
    }
}
